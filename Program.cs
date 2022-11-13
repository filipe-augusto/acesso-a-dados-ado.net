﻿using System;
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
            //   DapperInsert();
            // Dapper();
            Conexao();

        }


        static void Dapper()
        {
            using (var con = new SqlConnection(connectionStirng))
            {
                var categories = con.Query<Category>("SELECT [Id], [Title] FROM [Category]");
                foreach (var category in categories)
                    System.Console.WriteLine($"{category.Id}-{category.Title}");
            }
        }

        static void SelectDapper(SqlConnection connection)
        {
            var query = "SELECT [Id], [Title] FROM [Category]";
            var categories = connection.Query<Category>(query);
            foreach (var category in categories)
                System.Console.WriteLine($"{category.Id}-{category.Title}");
        }
        static void DapperUpdate(SqlConnection connection)
        {

            var updateSql = $"UPDATE [Category] SET [Title]=@title WHERE [Id]=@Id";
            var rowns = connection.Execute(updateSql, new
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "FrontEnd 2023"
            });

        }

        static void DapperInsert(SqlConnection connection)
        {
            Category category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS 2";
            category.Url = "amazon 5";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Could 2";
            category.Featured = false;

            var query = $"INSERT INTO [Category] values(" +
                "@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";
            var rows = connection.Execute(query, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Description,
                category.Order,
                category.Featured
            });
            System.Console.WriteLine($"LINHAS INSERIDAS {rows}");
        }
        static void Conexao()
        {
            using (var con = new SqlConnection(connectionStirng))
            {
                DapperInsert(con);
                DapperUpdate(con);
                SelectDapper(con);
            }
        }

        static void DapperInsert()
        {
            Category category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Could";
            category.Featured = false;

            var insertSql = $"INSERT INTO [Category] values(" +
                "@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";
            using (var con = new SqlConnection(connectionStirng))
            {
                var rows = con.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Description,
                    category.Order,
                    category.Featured
                });
                System.Console.WriteLine($"LINHAS INSERIDAS {rows}");
            }
        }

        static void ModoAntigo()
        {

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
