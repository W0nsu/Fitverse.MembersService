using System;
using System.Linq;
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
	public class EditMembershipReciver : BackgroundService
	{
		private readonly string _hostName;
		private readonly string _password;
		private readonly IServiceProvider _provider;
		private readonly string _userName;

		private IModel _channel;
		private IConnection _connection;

		public EditMembershipReciver(IOptions<RabbitMqConfiguration> rabbitMqOptions, IServiceProvider serviceProvider)
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
			_channel.QueueDeclare("MS_EditMembership", false, false, false, null);
			_channel.QueueBind("MS_EditMembership", "memberships", "editMembership", null);
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
				var editedMembershipEntity = JsonConvert.DeserializeObject<Membership>(content);

				var membershipEntity = dbContext
					.Memberships
					.First(x => x.MembershipId == editedMembershipEntity.MembershipId);

				membershipEntity.Name = editedMembershipEntity.Name;
				membershipEntity.PeriodType = editedMembershipEntity.PeriodType;
				membershipEntity.Duration = editedMembershipEntity.Duration;
				membershipEntity.TerminationPeriod = editedMembershipEntity.TerminationPeriod;
				membershipEntity.InstallmentPrice = editedMembershipEntity.InstallmentPrice;

				_ = dbContext.SaveChanges();

				_channel.BasicAck(ea.DeliveryTag, false);
			};

			_channel.BasicConsume("MS_EditMembership", false, consumer);

			return Task.CompletedTask;
		}
	}
}