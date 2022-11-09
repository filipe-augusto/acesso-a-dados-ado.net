using System;
using Microsoft.Data.SqlClient;

namespace baltaDataAccess
{

    class Program
    {
        const string connectionStirng = @"Server=192.168.99.100\sqlserver,1433;Database=balta;
        User ID=sa; Password=1q2w3e4r@#$";
        //se vc colocar a autenticação do windows será integrated security=sspi 
        //dotnet add package microsoft.data.sqlclient add o pacote
        // Microsoft.Data.SqlClient (Nuget)
        static void Main(string[] args)
        {
            #region old

            using (var con = new SqlConnection(connectionStirng))
            {
                System.Console.WriteLine("conectado.");
                con.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT [ID], [TITLE] FROM [CATEGORY]";
                    var reader = command.ExecuteReader();
                   // command.ExecuteNonQuery para proc
                    while (reader.Read())
                    {
                        System.Console.WriteLine($"{reader.GetGuid(0)} - " +
                        $"{reader.GetString(1)}");
                    }

                }
            }
            /*            var connection = new SqlConnection();
                       connection.Open();
                       connection.Close();
                       connection.Dispose(); */
            #endregion


            System.Console.WriteLine("hello word");
        }

    }
}
