﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");
var factory = new ConnectionFactory()
{
    HostName = "localhost"
};
using (var connection = factory.CreateConnection())

using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "saudacao_1",
                         durable: false,
                         autoDelete: false,
                         exclusive: false,
                         arguments: null
                         );
    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [x] Recebida : {message}");
    };

    channel.BasicConsume(queue: "saudacao_1",
                         autoAck:true,
                         consumer: consumer);
    Console.ReadLine();
}