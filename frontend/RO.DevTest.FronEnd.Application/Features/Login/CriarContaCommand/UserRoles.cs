using System.ComponentModel;

namespace RO.DevTest.FronEnd.Application.Features.Login.CriarContaCommand;

public enum UserRoles
{
	[Description("Admin")]
	Admin = 0,
	[Description("Customer")]
	Customer = 1,
}