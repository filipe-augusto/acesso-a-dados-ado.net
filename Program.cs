using System;
using System.Data;
using System.Collections.Generic;
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
        static void DapperManyInsert(SqlConnection connection)
        {
            Category category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS 2";
            category.Url = "amazon 5";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Could 2";
            category.Featured = false;

            Category category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria nova ";
            category2.Url = "Categoria nova";
            category2.Description = "Categoria destinada a serviços do AWS";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = true;

            var query = $"INSERT INTO [Category] values(" +
                "@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";
            var rows = connection.Execute(query, new[]
             {
                new {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Description,
                category.Order,
                category.Featured
            },
         new {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Description,
                category2.Order,
                category2.Featured
            }
             }
            );
            System.Console.WriteLine($"LINHAS INSERIDAS {rows}");
        }

        static void Conexao()
        {
            //Console.Clear();
            using (var con = new SqlConnection(connectionStirng))
            {
                // DapperInsert(con);
                // DapperManyInsert(con);
                // DapperUpdate(con);
                //SelectDapper(con);
                // ExecuteReadProcedure(con);
                //ExecuteProcedure(con);
                // ExecuteScalar(con);
                ReadView(con);
                //     Console.Clear();
                //OneToOne(con);
                //OneToMany(con);
                // QueryMultiple(con);
                // SelectIn(con);
                Transaction(con);
                Like(con);
            }
        }

        static void DapperInsertOld()
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

        static void ExecuteProcedure(SqlConnection connection)
        {

            var sql = "[spDeleteStudent]";
            var pars = new { StudentId = "93eeb410-69f3-40e4-955b-32a4f28dd7ec" };
            var affectedRows = connection.Execute(sql,
              pars,
              commandType: CommandType.StoredProcedure);
            System.Console.WriteLine($"linhas afetadas: {affectedRows}");
        }

        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[spGetCoursesByCategory]";
            var pars = new { CategoryId = "25d510c8-3108-44c2-86c5-924d9832aa8c" };
            var courses = connection.Query<Category>(procedure, pars, commandType: CommandType.StoredProcedure);
            foreach (var item in courses)
            {
                Console.WriteLine($"{item.Id} {item.Title}");
            }
        }

        static void ExecuteScalar(SqlConnection connection)
        {
            Category category = new Category();

            category.Title = "execute escalar AWS";
            category.Url = "execute";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "execute  escalar";
            category.Featured = false;

            var insertSql = $"INSERT INTO [Category] OUTPUT inserted.[Id] values(" +
                "NEWID(), @Title, @Url, @Summary, @Order, @Description, @Featured) " +
                " SELECT SCOPE_IDENTITY()";

            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Description,
                category.Order,
                category.Featured
            });
            System.Console.WriteLine($"nova categoria inserida {id}");
        }

        static void ReadView(SqlConnection connection)
        {
            var sql = "SELECT * FROM [vwCourses]";
            var courses = connection.Query(sql);
            foreach (var item in courses)
                System.Console.WriteLine($"{item.Tag}-{item.Title}");
        }

        static void OneToOne(SqlConnection connection)
        {
            var sql = @"SELECT * FROM [CareerItem] INNER JOIN [Course] " +
            @" ON [CareerItem].[CourseId]  = [Course].[Id]";

            var items = connection.Query<CareerItem, Course, CareerItem>(
                sql,
                (careerItem, course) =>
                {
                    careerItem.Course = course;
                    return careerItem;
                },
                splitOn: "Id"
                );
            foreach (var item in items)
            {
                System.Console.WriteLine($" item: {item.Title}  Curso: {item.Course.Title}");
            }
        }

        static void OneToMany(SqlConnection connection)
        {
            var sql = "SELECT [Career].[Id],[Career].[Title],[CareerItem].[CareerId]," +
            "[CareerItem].[Title] FROM [Career]  INNER JOIN [CareerItem] " +
            "ON [CareerItem].[CareerId] = [Career].[Id] ORDER BY [Career].[Title]";
            var careers = new List<Career>();
            var items = connection.Query<Career, CareerItem, Career>(
                sql,
                (career, item) =>
                {
                    var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
                    if (car == null)
                    {
                        car = career;
                        car.Items.Add(item);
                        careers.Add(car);
                    }
                    else
                    {
                        car.Items.Add(item);
                    }

                    career.Items.Add(item);
                    return career;
                },
                splitOn: "CareerId"
            );

            foreach (var career in careers)
            {
                System.Console.WriteLine($"{career.Title}");
                foreach (var item in career.Items)
                {
                    System.Console.WriteLine($"     {item.Title}");
                }

            }

        }

        static void QueryMultiple(SqlConnection connection)
        {
            var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";
            using (var multi = connection.QueryMultiple(query))
            {
                var categories = multi.Read<Category>();
                var courses = multi.Read<Course>();

                foreach (var item in categories)
                {
                    Console.WriteLine(item.Title);
                }
                foreach (var item in courses)
                {
                    System.Console.WriteLine("-" + item.Title);
                }
            }
        }

        static void SelectIn(SqlConnection connection)
        {
            /*  var query = "SELECT * from [Category] where id in " +
              "('fbb13482-2c41-40dd-99d6-1e7370a73466','af3407aa-11ae-4621-a2ef-2028b85507c4')"; */
            var query = "SELECT * from [Category] where [Id] in @Id";
            var careers = connection.Query<Career>(query, new
            {
                id = new[]{
                    "fbb13482-2c41-40dd-99d6-1e7370a73466",
                    "af3407aa-11ae-4621-a2ef-2028b85507c4"
                }
            });
            foreach (var item in careers)
            {
                System.Console.WriteLine($" carrer {item.Title}");
            }
        }

        static void Like(SqlConnection connection)
        {
            var term = "api";
            var query = "SELECT * FROM [Course] WHERE [Title] LIKE @exp";
            var courses = connection.Query<Course>(query, new
            {
                exp = $"%{term}%"
            });
            foreach (var item in courses)
            {
                System.Console.WriteLine($" course {item.Title}");
            }
        }

        static void Transaction(SqlConnection connection)
        {
            Category category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "NAO QUERO";
            category.Url = "NAO QUERO";
            category.Description = "NAO QUERO";
            category.Order = 8;
            category.Summary = "ANAO QUERO";
            category.Featured = false;

            var query = $"INSERT INTO [Category] values(" +
                "@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";


            using (var transaction = connection.BeginTransaction())
            {
                var rows = connection.Execute(query, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Description,
                    category.Order,
                    category.Featured
                }, transaction);
                transaction.Commit();
                System.Console.WriteLine($"LINHAS INSERIDAS {rows}");
                transaction.Rollback();
            }

        }

    }
}
