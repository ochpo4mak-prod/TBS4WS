using UnityEngine;
using MySql.Data.MySqlClient;

public class SQLscript : MonoBehaviour
{
    private static MySqlConnection _connection;

    void Start()
    {
        if (!globals.isGenerateMap)
        {
            SetupSQLConnection();
            RequestExecution("DELETE FROM playerinfo");
        }
        // CloseSQLConnection();
    }

    public static void RequestExecution(string commandText)
    {
        if (_connection != null)
        {
            MySqlCommand command = _connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }
    }

    public static string RequestSelectExecution(string commandText)
    {
        string answer = "error";
        
        if (_connection != null)
        {
            MySqlCommand command = _connection.CreateCommand();
            command.CommandText = commandText;
            try
            {
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    answer = reader.GetString(0);
                }

                reader.Dispose();
            }
            catch (System.Exception e)
            {
                Debug.LogError("My SQL Error:" + e.ToString());
            }
        }
        return answer;
    }

    private void SetupSQLConnection()
    {
        if (_connection == null)
        {
            string connectionString = "SERVER=localhost;" + "DATABASE=unity;" + "UID=root;" + "PASSWORD=1235;";
            try
            {
                _connection = new MySqlConnection(connectionString);
                _connection.Open();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("MySQL Error: " + ex.ToString());
            }
        }
    }

    private void CloseSQLConnection()
    {
        if (_connection != null)
        {
            _connection.Close();
        }
    }
}