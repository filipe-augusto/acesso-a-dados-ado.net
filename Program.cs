using System;
using baltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace baltaDataAccess
{

    class Program
    {
        const string connectionStirng = @"Server=192.168.99.100\sqlserver,1433;Database=balta;
        User ID=sa; Password=1q2w3e4r@#$";
        const string conectStringJob = @"Server=IM-BRS-NT1071\MSSQLSERVER01; Integrated Security=SSPI; Database=balta";
        //se vc colocar a autenticação do windows será integrated security=sspi 
        //dotnet add package microsoft.data.sqlclient add o pacote
        // Microsoft.Data.SqlClient (Nuget)
        //dotnet add package dapper
        static void Main(string[] args)
        {                                        
                Dapper();
           
        }


        static void Dapper(){
        using(var con = new SqlConnection(connectionStirng)){
            var categories =  con.Query<Category>("SELECT [Id], [Title] FROM [Category]");   
                foreach(var category in categories) 
                System.Console.WriteLine($"{category.Id}-{category.Title}");        
             }
        }

        static void ModoAntigo(){
           
        using (var con = new SqlConnection(connectionStirng))
        {
            System.Console.WriteLine("conectado.");
            con.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = con;
                command.CommandType = System.Data.CommandType.Text;
        
                var reader = command.ExecuteReader();
            // command.ExecuteNonQuery para proc
                while (reader.Read())
                {
                    System.Console.WriteLine($"{reader.GetGuid(0)} - " +
                    $"{reader.GetString(1)}");
                }
            }
        }
        }

    }
}
