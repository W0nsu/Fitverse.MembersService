using System;
using Fitverse.MembersService.Interfaces;
using Fitverse.MembersService.Models;
using Fitverse.Shared.MessageBus;
using Microsoft.Extensions.Options;

namespace Fitverse.MembersService.MessageBus.Senders
{
	public class EditMemberSender : IEditMemberSender
	{
		private readonly IOptions<RabbitMqConfiguration> _rabbitMqOptions;

		public EditMemberSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
		{
			_rabbitMqOptions = rabbitMqOptions;
		}

		public void EditMember(Member member)
		{
			var exchangeConfig = new Tuple<string, string>("members", "editMember");
			SendEventHandler.SendEvent(member, _rabbitMqOptions, exchangeConfig);
		}
	}
}