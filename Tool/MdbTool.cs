using System.Data.OleDb;
using System.IO;

namespace Test.Tool;

public static class MdbTool
{
    public static List<string> Select(string sql)
    {
        var projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent.Parent.FullName;
        var mdbFilePath = Path.Combine(projectDirectory, "../Files/Test.mdb");
        var _connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={mdbFilePath};";

        using (var connection = new OleDbConnection(_connectionString))
        {
            try
            {
                connection.Open();
                var command = new OleDbCommand(sql, connection);
                var reader = command.ExecuteReader();

                //将结果封装到List<string>集合中
                var result = new List<string>();
                while (reader.Read())
                    for (var i = 0; i < reader.FieldCount; i++)
                        result.Add(reader[i].ToString());

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return null;
        }
    }
}