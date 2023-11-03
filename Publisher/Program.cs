using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };  // definimos uma conexão com um nó RabbitMQ em localhost

using (var connection = factory.CreateConnection())  // abrimos uma conexão com um nó RabbitMQ

using (var channel = connection.CreateModel())  // criamos um canal onde vamos definir uma fila, uma mensagem e publicar a mensagem
{
    channel.QueueDeclare(queue: "saudacao_1",  // nome da fila
                         durable: false,  // se for true, a fila permanece ativa após o servidor ser reiniciado
                         exclusive: false,  // se for true ela só pode ser acessada via conexão atual e são excluídas ao fechar a conexão
                         autoDelete: false, // se for true ela será deletada automaticamente após os consumidores usarem a fila
                         arguments: null);

    string message = "Bem vindo ao RabbitMQ";  // criando a mensagem

    var body = Encoding.UTF8.GetBytes(message);  // codificamos a mensagem para um array de bytes, pois uma mensagem RabbitMQ é um bloco de dado binário

    channel.BasicPublish(exchange: "",  // publicamos a mensagem informando a fila e o corpo da mensagem
                        routingKey: "saudacao_1",
                        basicProperties: null,
                        body: body);

    Console.WriteLine($"[X] Enviada: {message}");  // exibindo no terminal
}

Console.ReadLine();

// a mensagem estará visivel em localhost:15672 na aba Queues and Streams