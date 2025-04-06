using Microsoft.AspNetCore.SignalR.Client;
using catch_up_mobile.SQLite;
using catch_up_mobile.Dtos;
using Microsoft.Extensions.Logging;
using catch_up_mobile.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace catch_up_mobile.Services
{
    public class SignalRService
    {
        private HubConnection? _connection;
        private string _hubUrl = "notificationHub";

        private readonly CatchUpdbContext _dbContext;

        public SignalRService(CatchUpdbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _hubUrl = configuration["ApiSettings:Url"] + _hubUrl;
        }

        public async Task StartAsync()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true //SSL ingorned
            };

            _connection = new HubConnectionBuilder()
                .WithUrl(_hubUrl, options =>
                {
                    options.HttpMessageHandlerFactory = _ => httpClientHandler;
                    options.AccessTokenProvider = () => _dbContext.GetAccessToken();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddDebug();
                })
                .Build();

            _connection.Closed += async (error) =>
            {
                Console.WriteLine("SignalR connection closed: " + error?.Message);
                await Task.Delay(5000);
                await StartAsync(); 
            };

            try
            {
                await _connection.StartAsync();
                Console.WriteLine("SignalR connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting SignalR connection: " + ex.Message);
            }
        }

        public async Task StopAsync()
        {
            if (_connection != null)
            {
                await _connection.StopAsync();
                Console.WriteLine("SignalR connection stopped");
            }
        }

        public HubConnection GetHubConncetion()
        {
            return _connection;
        }
    }
}