using System;

namespace baltaDataAccess
{

    class Program
    {
       const string connectionStirng = @"Server=192.168.99.100\sqlserver,1433;Database=balta;
        User ID=sa; Password=1q2w3e4r@#$";
       //se vc colocar a autenticação do windows será integrated security=sspi 
        static void Main(string[] args)
        {
        //dotnet add package microsoft.data.sqlclient add o pacote
            // Microsoft.Data.SqlClient (Nuget)
            System.Console.WriteLine("hello word");
        }

    }
}
