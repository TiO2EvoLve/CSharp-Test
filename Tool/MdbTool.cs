using System.Data.OleDb;

namespace Test.Tool;

public static class MdbTool
{
    public static List<string> Select(string sql)
    {
        string projectDirectory = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent.Parent.FullName;
        string mdbFilePath = System.IO.Path.Combine(projectDirectory, "../Files/Test.mdb");
        string _connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={mdbFilePath};";
        
        using (OleDbConnection connection = new OleDbConnection(_connectionString))
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                //将结果封装到List<string>集合中
                List<string> result = new List<string>();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result.Add(reader[i].ToString());
                    }
                }
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