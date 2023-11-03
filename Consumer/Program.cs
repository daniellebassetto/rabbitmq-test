using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };  // definimos uma conexão com um nó RabbitMQ em localhost

using (var connection = factory.CreateConnection())  // abrimos uma conexão com um nó RabbitMQ

using (var channel = connection.CreateModel())  // declaramos a fila a partir da qual iremos consumir as mensagens
{
    channel.QueueDeclare(queue: "saudacao_1",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);  // solicita a entrega das mensagens de forma assíncrona e fornece um retorno de chamada

    consumer.Received += (model, ea) =>  // recebe a mensagem da fila, converte para string e imprime no console
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"[X] Recebida: {message}");
    };

    channel.BasicConsume(queue: "saudacao_1",  // indicando o consumo da mensagem para que ela saia da fila
                         autoAck: true,
                         consumer: consumer);
}

// a mensagem estará visivel em localhost:15672 na aba Queues and Streams