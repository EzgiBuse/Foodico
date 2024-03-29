﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Foodico.Services.AuthAPI.RabbitMQSender
{
    public class RabbitMQAuthMessageSender : IRabbitMQAuthMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        public RabbitMQAuthMessageSender()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }
        public void SendMessage(object message, string queueName)
        {
            var factory = new ConnectionFactory() 
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password 
            };
            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            
                channel.QueueDeclare(queueName,false,false,false,null);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            
        }
    }
}
