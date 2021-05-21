using System;
using Fitverse.MembersService.Interfaces;
using Fitverse.MembersService.Models;
using Fitverse.Shared.MessageBus;
using Microsoft.Extensions.Options;

namespace Fitverse.MembersService.MessageBus.Senders
{
	public class AddAgreementSender : IAddAgreementSender
	{
		private readonly IOptions<RabbitMqConfiguration> _rabbitMqOptions;

		public AddAgreementSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
		{
			_rabbitMqOptions = rabbitMqOptions;
		}

		public void SendAgreement(Agreement agreement)
		{
			var exchangeConfig = new Tuple<string, string>("agreements", "addAgreement");
			SendEventHandler.SendEvent(agreement, _rabbitMqOptions, exchangeConfig);
		}
	}
}