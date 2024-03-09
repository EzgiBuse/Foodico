
using Foodico.Services.EmailAPI.Models;
using Foodico.Services.EmailAPI.Models.Dto;
using Foodico.Services.EmailAPI.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace Foodico.Services.EmailAPI.RabbitMQConsumer
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        private IConnection _connection;
        private IModel _channel;
        public RabbitMQConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("OrderConfirmationEmail", false, false, false, null);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());


                var email = JsonConvert.DeserializeObject<string>(content); 
                 HandleMessage(email).GetAwaiter().GetResult(); 
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("OrderConfirmationEmail", false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleMessage(string email)
        {
           _emailService.SendOrderEmailAsync(email);
        }
    }
}
