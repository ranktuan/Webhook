using RabbitMQ.Client;

namespace MinvoiceWebhook.Models
{
    public interface IRabbitMQConnection
    {
        IConnection CreateConnection();
    }
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConfiguration _configuration;

        public RabbitMQConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"],
                UserName = _configuration["RabbitMQ:Username"],
                Password = _configuration["RabbitMQ:Password"],
                VirtualHost = _configuration["RabbitMQ:VirtualHost"]
            };

            return factory.CreateConnection();

        }
    }
}
