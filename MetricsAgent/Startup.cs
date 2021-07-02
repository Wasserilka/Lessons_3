using MetricsAgent.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(10,10)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(50,20)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(75,40)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(20,4)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(30,5)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(60,8)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(12,11)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(45,21)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(89,31)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(22,5)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(33,6)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(44,7)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(3,5)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(6,55)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(9,80)";
                command.ExecuteNonQuery();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
