using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using DeepSeek.Sdk;
using Markdig.Wpf;
using Newtonsoft.Json;
using Test.ViewModels;

namespace Test;

public partial class DeepSeekTest : Window
{
    public DeepSeekTest()
    {
        InitializeComponent();
    }
    
    readonly string apikey = "sk-3fe186a7379443be804ecc2d79d0ac7c";// api密钥
    private static StringBuilder resultMsg = new StringBuilder();
    private string question = "";
    
    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        tip.Text = "请稍后。。。";
        question = input.Text;
        // 创建ds对象
        var ds = new DeepSeek.Sdk.DeepSeek(apikey);

        Task.Run(async () =>
        {
            var chatReq = new ChatRequest
            {
                Messages = new List<ChatRequest.MessagesType>
                {
                    new()
                    {
                        Role = ChatRequest.RoleEnum.System,
                        Content = question
                    }
                },
                Model = ChatRequest.ModelEnum.DeepseekChat,
                Stream = true
            };

            resultMsg.Clear(); // 拼接完成则清空, 进行下一轮拼接

            await ds.ChatStream(chatReq,
                openedCallBack: (state) => // 打开状态
                {
                    Console.WriteLine(state);
                },
                closedCallBack: (state) => // 关闭状态
                {
                    Console.WriteLine(state);
                },
                msgCallback: (res) => // 接收信息
                {
                    string msg = res.Choices.FirstOrDefault()?.Delta?.Content;
                    resultMsg.Append(msg);
                    Console.WriteLine(msg);
                },
                errorCallback: (ex) => // 异常处理
                {
                    Console.WriteLine(ex);
                });

            await Task.CompletedTask;
        }).GetAwaiter().GetResult(); 
        // 这里可以将结果显示在UI上
        var flowDocument = Markdown.ToFlowDocument(resultMsg.ToString());
        // 在 RichTextBox 中显示转换后的内容
        output.Document = flowDocument;
        tip.Text = "";
    }
    
    
}