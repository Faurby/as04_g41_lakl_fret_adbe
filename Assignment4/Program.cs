using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var configuration = LoadConfiguration();
            var connectionString = configuration.GetConnectionString("Kanban");

        }

        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>();

            return builder.Build();
        }

    }
}
