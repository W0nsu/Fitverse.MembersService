using System;
using Fitverse.MembersService.Interfaces;
using Fitverse.Shared.MessageBus;
using Microsoft.Extensions.Options;

namespace Fitverse.MembersService.MessageBus.Senders
{
	public class DeleteMemberSender : IDeleteMemberSender
	{
		private readonly IOptions<RabbitMqConfiguration> _rabbitMqOptions;

		public DeleteMemberSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
		{
			_rabbitMqOptions = rabbitMqOptions;
		}

		public void DeleteMember(int memberId)
		{
			var exchangeConfig = new Tuple<string, string>("members", "deleteMember");
			SendEventHandler.SendEvent(memberId, _rabbitMqOptions, exchangeConfig);
		}
	}
}