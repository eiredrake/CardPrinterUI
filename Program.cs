using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms.VisualStyles;
using CardPrinterUI.interfaces;
using GuardNet;
using System.Data;
using Npgsql;
using CardPrinterUI.models;

namespace CardPrinterUI

{
    internal static class Program
    { 

        private static IConfigurationRoot InitializeConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app.config.json", optional: false, reloadOnChange: false)
                .Build();
        }

        private static IHost InitializeDependencyInjection(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var configuration = InitializeConfiguration();

            var connection_string = configuration.GetValue<string>("configuration:database:dead_market_prod:connection_string");

            builder.Services.AddSingleton<IConfigurationRoot>(configuration);
            builder.Services.AddSingleton<IForm, masterForm>();
            builder.Services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(connection_string));
            builder.Services.AddScoped<IDataStore, DataStore>();

            return builder.Build();
        }


        [STAThread]
        static void Main(string[] args)
        {
            var host = InitializeDependencyInjection(args);

            try
            {
                var master_form = host.Services.GetService<IForm>();
                Guard.NotNull(master_form, nameof(master_form));

                Application.Run(master_form?.GetForm());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}