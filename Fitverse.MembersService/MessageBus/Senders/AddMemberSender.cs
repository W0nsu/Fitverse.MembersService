using System;
using Fitverse.MembersService.Interfaces;
using Fitverse.MembersService.Models;
using Fitverse.Shared.MessageBus;
using Microsoft.Extensions.Options;

namespace Fitverse.MembersService.MessageBus.Senders
{
	public class AddMemberSender : IAddMemberSender
	{
		private readonly IOptions<RabbitMqConfiguration> _rabbitMqOptions;

		public AddMemberSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
		{
			_rabbitMqOptions = rabbitMqOptions;
		}

		public void SendMember(Member member)
		{
			var exchangeConfig = new Tuple<string, string>("members", "addMember");
			SendEventHandler.SendEvent(member, _rabbitMqOptions, exchangeConfig);
		}
	}
}