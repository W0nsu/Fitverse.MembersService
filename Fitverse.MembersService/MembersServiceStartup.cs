using System;
using System.Text;
using Fitverse.MembersService.Data;
using Fitverse.MembersService.Interfaces;
using Fitverse.MembersService.MessageBus.Recivers;
using Fitverse.MembersService.MessageBus.Senders;
using Fitverse.Shared;
using Fitverse.Shared.MessageBus;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Fitverse.MembersService
{
	public class MembersServiceStartup : SharedStartup
	{
		public MembersServiceStartup(IConfiguration configuration, IWebHostEnvironment environment) : base(
			configuration,
			environment)
		{
		}

		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				//services.AddCors();

				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Fitverse.MembersService",
					Version = "v1",
					Description = "ASP.NET Core Web API for Fitverse, complex fitness management solution",
					Contact = new OpenApiContact
					{
						Name = "Paweł Wąsowski",
						Email = "pwasowski@edu.cdv.pl",
						Url = new Uri("https://www.linkedin.com/in/pawelwasowski/")
					}
				});
			});
			
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ClockSkew = TimeSpan.Zero,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["Jwt:Issuer"],
						ValidAudience = Configuration["Jwt:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
					};
				});

			base.ConfigureServices(services);
			services.AddDbContext<MembersContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("AzureDb")));

			services.AddMediatR(typeof(MembersServiceStartup));

			services.AddValidatorsFromAssembly(typeof(MembersServiceStartup).Assembly);

			services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));


			services.AddTransient<IAddMemberSender, AddMemberSender>();
			services.AddTransient<IAddAgreementSender, AddAgreementSender>();
			services.AddTransient<IDeleteMemberSender, DeleteMemberSender>();
			services.AddTransient<IEditMemberSender, EditMemberSender>();

			services.AddHostedService<AddMembershipReciver>();
			services.AddHostedService<DeleteMembershipReciver>();
			services.AddHostedService<EditMembershipReciver>();
			services.AddHostedService<SignUpForClassReciver>();
			services.AddHostedService<SignOutOfClassReciver>();
		}
	}
}