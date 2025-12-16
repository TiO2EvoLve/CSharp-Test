using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Test.Page;

public partial class 测试 : Window
{
    public 测试()
    {
        InitializeComponent();
    }

    private void Test1(object sender, RoutedEventArgs e)
    {
        try
        {
            // ========== 步骤1：模拟服务端生成并编码盐值 ==========
            // 生成16字节随机盐值（服务端初始化时执行一次）
            byte[] serverOriginalSalt = M1CardKeyHelper.Generate16ByteRandomSalt();
            // 编码为Base64字符串（服务端存储/传输给客户端）
            string serverSaltBase64 = M1CardKeyHelper.EncodeSaltToBase64(serverOriginalSalt);
            Console.WriteLine("=== 服务端盐值处理 ===");
            Console.WriteLine($"原始16字节盐值（十六进制）：{M1CardKeyHelper.BytesToHexString(serverOriginalSalt)}");
            Console.WriteLine($"编码后的Base64字符串：{serverSaltBase64}\n");

            // ========== 步骤2：模拟客户端接收Base64盐值并解析 ==========
            byte[] decodedSalt = M1CardKeyHelper.DecodeSaltFromBase64(serverSaltBase64);
            Console.WriteLine("=== 客户端盐值解析 ===");
            Console.WriteLine($"解析后的盐值（十六进制）：{M1CardKeyHelper.BytesToHexString(decodedSalt)}");
            Console.WriteLine($"盐值解析验证：{(M1CardKeyHelper.BytesToHexString(decodedSalt) == M1CardKeyHelper.BytesToHexString(serverOriginalSalt) ? "成功" : "失败")}\n");

            // ========== 步骤3：模拟读取M1卡UID并生成密钥 ==========
            // 模拟读卡器读取4字节UID
            //95A06068
            byte[] cardUid = { 0x95, 0xA0, 0x60, 0x68 };
            // 生成6字节密钥
            byte[] oneCardKey = M1CardKeyHelper.Generate6ByteKey(cardUid, decodedSalt);

            // ========== 输出最终结果 ==========
            Console.WriteLine("=== M1卡密钥生成结果 ===");
            Console.WriteLine($"M1卡UID（十六进制）：{M1CardKeyHelper.BytesToHexString(cardUid)}");
            Console.WriteLine($"生成的6字节密钥（十六进制）：{M1CardKeyHelper.BytesToHexString(oneCardKey)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"执行失败：{ex.Message}");
            Console.WriteLine($"异常详情：{ex.StackTrace}");
        }

        Console.ReadLine();
    }
    /// <summary>
    /// M1卡一卡一密生成工具类（适配Base64编码的盐值解析）
    /// </summary>
    public static class M1CardKeyHelper
    {
        /// <summary>
        /// 生成的密钥长度（M1卡Key A/B均为6字节）
        /// </summary>
        private const int TargetKeyLength = 6;

        #region 核心方法1：解析Base64编码的盐值（Base64→十六进制字符串→字节数组）
        /// <summary>
        /// 解析服务端传入的Base64盐值字符串，还原为原始字节数组
        /// 适配流程：原始盐值(16字节)→十六进制字符串→Base64→传输→本方法解析回字节数组
        /// </summary>
        /// <param name="saltBase64Str">服务端传入的Base64编码盐值字符串</param>
        /// <returns>原始盐值字节数组（16字节）</returns>
        /// <exception cref="ArgumentNullException">Base64字符串为空</exception>
        /// <exception cref="FormatException">Base64格式错误/十六进制格式错误/盐值长度非法</exception>
        public static byte[] DecodeSaltFromBase64(string saltBase64Str)
        {
            // 1. 校验Base64字符串非空
            if (string.IsNullOrEmpty(saltBase64Str))
                throw new ArgumentNullException(nameof(saltBase64Str), "服务端传入的Base64盐值不能为空");

            try
            {
                // 2. Base64解码为字节数组（对应原始十六进制字符串的ASCII字节）
                byte[] hexStrBytes = Convert.FromBase64String(saltBase64Str);
                // 3. 字节数组转十六进制字符串（ASCII编码还原）
                string saltHexStr = Encoding.ASCII.GetString(hexStrBytes);

                // 4. 校验十六进制字符串合法性（长度为32位=16字节×2）
                if (saltHexStr.Length != 32)
                    throw new FormatException($"盐值十六进制字符串长度应为32位（16字节），实际为{saltHexStr.Length}位");

                // 5. 十六进制字符串转原始盐值字节数组（16字节）
                return HexStringToBytes(saltHexStr);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Base64盐值解析失败：{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new FormatException($"盐值解析异常：{ex.Message}", ex);
            }
        }

        #endregion

        #region 核心方法2：基于UID+解析后的盐值生成6字节密钥
        /// <summary>
        /// 基于M1卡UID和解析后的服务端盐值生成6字节一卡一密密钥
        /// </summary>
        /// <param name="cardUidBytes">M1卡UID字节数组（4/7字节）</param>
        /// <param name="saltBytes">解析后的原始盐值字节数组（16字节）</param>
        /// <returns>6字节密钥</returns>
        /// <exception cref="ArgumentNullException">UID/盐值为空</exception>
        /// <exception cref="ArgumentException">UID/盐值长度非法</exception>
        public static byte[] Generate6ByteKey(byte[] cardUidBytes, byte[] saltBytes)
        {
            // 1. 入参严格校验
            if (cardUidBytes == null)
                throw new ArgumentNullException(nameof(cardUidBytes), "M1卡UID字节数组不能为空");
            if (saltBytes == null)
                throw new ArgumentNullException(nameof(saltBytes), "盐值字节数组不能为空");

            if (cardUidBytes.Length != 4 && cardUidBytes.Length != 7)
                throw new ArgumentException("M1卡UID长度仅支持4字节（常规）或7字节（特殊）", nameof(cardUidBytes));
            if (saltBytes.Length != 16)
                throw new ArgumentException("盐值长度必须为16字节", nameof(saltBytes));

            // 2. 拼接UID + 盐值
            byte[] combinedData = new byte[cardUidBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(cardUidBytes, 0, combinedData, 0, cardUidBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, combinedData, cardUidBytes.Length, saltBytes.Length);

            // 3. SHA-256哈希计算（输出32字节，不可逆）
            byte[] sha256Hash;
            using (SHA256 sha256 = SHA256.Create())
            {
                sha256Hash = sha256.ComputeHash(combinedData);
            }

            // 4. 截取前6字节作为最终密钥（规则统一即可）
            byte[] finalKey = new byte[TargetKeyLength];
            Buffer.BlockCopy(sha256Hash, 0, finalKey, 0, TargetKeyLength);

            return finalKey;
        }
        #endregion

        #region 辅助方法：字节数组↔十六进制字符串
        /// <summary>
        /// 字节数组转大写十六进制字符串（无分隔符）
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>十六进制字符串</returns>
        public static string BytesToHexString(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2")); // X2=两位大写，补零
            }
            return sb.ToString();
        }

        /// <summary>
        /// 十六进制字符串转字节数组（严格校验）
        /// </summary>
        /// <param name="hexStr">大写/小写十六进制字符串，无分隔符</param>
        /// <returns>字节数组</returns>
        /// <exception cref="ArgumentException">格式非法</exception>
        public static byte[] HexStringToBytes(string hexStr)
        {
            if (string.IsNullOrEmpty(hexStr) || hexStr.Length % 2 != 0)
                throw new ArgumentException("十六进制字符串不能为空且长度为偶数", nameof(hexStr));

            byte[] bytes = new byte[hexStr.Length / 2];
            for (int i = 0; i < hexStr.Length; i += 2)
            {
                if (!byte.TryParse(hexStr.Substring(i, 2), System.Globalization.NumberStyles.HexNumber, null, out byte b))
                {
                    throw new ArgumentException($"十六进制字符串包含非法字符：{hexStr.Substring(i, 2)}", nameof(hexStr));
                }
                bytes[i / 2] = b;
            }
            return bytes;
        }
        #endregion

        #region 工具方法：生成16字节随机盐值（服务端初始化用）
        /// <summary>
        /// 生成16字节安全随机盐值（服务端初始化盐值时调用）
        /// </summary>
        /// <returns>16字节随机盐值</returns>
        public static byte[] Generate16ByteRandomSalt()
        {
            byte[] salt = new byte[16];
            // 使用加密安全的随机数生成器（禁止用Random）
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// 服务端盐值编码（16字节→十六进制→Base64）
        /// </summary>
        /// <param name="saltBytes">16字节原始盐值</param>
        /// <returns>Base64编码的盐值字符串</returns>
        public static string EncodeSaltToBase64(byte[] saltBytes)
        {
            if (saltBytes == null || saltBytes.Length != 16)
                throw new ArgumentException("盐值必须为16字节", nameof(saltBytes));

            // 16字节→32位十六进制字符串
            string saltHex = BytesToHexString(saltBytes);
            // 十六进制字符串→ASCII字节→Base64
            byte[] hexBytes = Encoding.ASCII.GetBytes(saltHex);
            return Convert.ToBase64String(hexBytes);
        }
        #endregion
    }
}