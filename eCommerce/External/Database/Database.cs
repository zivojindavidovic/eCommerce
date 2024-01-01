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
        try
        {
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during insert: {ex}");
            return false;
        }
    }

    public bool update(string sql)
    {
        try
        {
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during update: {ex}");
            return false;
        }
    }

    public List<Dictionary<string, string>> select(string sql)
    {
        List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
        List<string> errors = new List<string>();

        try
        {
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> row = new Dictionary<string, string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i).ToString()!;
                        }

                        result.Add(row);
                    }
                }
            }
        }
        catch (Exception e)
        {
            errors.Add(e.ToString());
        }

        if (errors.Count > 0)
        {
            Dictionary<string, string> errorRow = new Dictionary<string, string>
        {
            { "error", errors[0] }
        };
            result.Add(errorRow);
        }

        return result;
    }
}