using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.MembersService.Data;
using Fitverse.MembersService.Models;
using Fitverse.Shared.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Fitverse.MembersService.MessageBus.Recivers
{
	public class AddMembershipReciver : BackgroundService
	{
		private readonly string _hostName;
		private readonly string _password;
		private readonly IServiceProvider _provider;
		private readonly string _userName;

		private IModel _channel;
		private IConnection _connection;

		public AddMembershipReciver(IOptions<RabbitMqConfiguration> rabbitMqOptions, IServiceProvider serviceProvider)
		{
			_provider = serviceProvider;

			_hostName = rabbitMqOptions.Value.Hostname;
			_userName = rabbitMqOptions.Value.UserName;
			_password = rabbitMqOptions.Value.Password;

			InitializeRabbitMqListener();
		}

		private void InitializeRabbitMqListener()
		{
			var factory = new ConnectionFactory
			{
				HostName = _hostName,
				UserName = _userName,
				Password = _password
			};

			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();

			_channel.ExchangeDeclare("memberships", "direct");
			_channel.QueueDeclare("MS_AddMembership", false, false, false, null);
			_channel.QueueBind("MS_AddMembership", "memberships", "addMembership", null);
		}


		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (ch, ea) =>
			{
				using var scope = _provider.CreateScope();
				var dbContext = scope.ServiceProvider.GetRequiredService<MembersContext>();

				var content = Encoding.UTF8.GetString(ea.Body.ToArray());
				var newMembershipEntity = JsonConvert.DeserializeObject<Membership>(content);

				_ = dbContext.Memberships.Add(newMembershipEntity);
				_ = dbContext.SaveChanges();

				_channel.BasicAck(ea.DeliveryTag, false);
			};

			_channel.BasicConsume("MS_AddMembership", false, consumer);

			return Task.CompletedTask;
		}
	}
}