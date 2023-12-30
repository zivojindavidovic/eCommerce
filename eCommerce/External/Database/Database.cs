using MySql.Data.MySqlClient;

namespace eCommerce.External.Database;

public class Database
{
    private string connectionString = "server=localhost;user=root;database=ecommerce;port=3306";
    private MySqlConnection connection;
    

    public Database()
    {
        connection = connect();
        connection.Open();
    }

    private MySqlConnection connect()
    {
        return new MySqlConnection(connectionString);
    }

    public bool insert(string sql)
    {
        bool ret = true;
        MySqlCommand command = new MySqlCommand(sql, connection);

        if (command.ExecuteNonQuery() > 0) {
            ret = false;
        }

        connection.Close();
        return ret;
    }

    public List<Dictionary<string, string>> select(string sql)
    {
        List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
        Dictionary<string, string> row = new Dictionary<string, string>();
        List<string> errors = new List<string>();

        using (connection) {
            try {
                using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            for (int i = 0; i < reader.FieldCount; i++) {
                                row[reader.GetName(i)] = reader.GetValue(i).ToString()!;
                            }
                            result.Add(row);
                        }
                    }
                }
            } catch (Exception e){
                errors.Add(e.ToString());
            }
        }

        if (errors.Count > 0) {
            row["error"] = errors[0];
            result.Add(row); 
        }
        
        connection.Close();
        return result;
    }
}