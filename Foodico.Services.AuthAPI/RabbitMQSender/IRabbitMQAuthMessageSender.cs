﻿namespace Foodico.Services.AuthAPI.RabbitMQSender
{
    public interface IRabbitMQAuthMessageSender
    {
        void SendMessage(Object message, string queueName);
    }
}
