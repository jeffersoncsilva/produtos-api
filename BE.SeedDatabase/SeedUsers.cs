using BE.Application.Contracts.Infrastructure;
using BE.Domain.Entities;
using BE.Domain.Enums;
using Bogus;

namespace BE.SeedDatabase;

public class SeedUsers(IIdentityAbstractor identity)
{
    public async Task Seed()
    {
        await SeedUsersAsync();
    }
    
    private async Task SeedUsersAsync()
    {
        var userAdmin = new User()
        {
            UserName = "admin@email.com",
            Email = "admin@email.com",
            Name = "user_admin"
        };
        var userAdminResult = await identity.CreateUserAsync(userAdmin, "123456");
        
        if (userAdminResult.Succeeded)
            await identity.AddToRoleAsync(userAdmin, UserRoles.Admin);
        
        var userUser = new User()
        {
            UserName = "user@email.com",
            Email = "user@email.com",
            Name = "user"
        };
        var userResult = await identity.CreateUserAsync(userUser, "123456");
        if (userResult.Succeeded)
            await identity.AddToRoleAsync(userUser, UserRoles.Customer);

        await CreateUsersAsync();
    }

    private async Task CreateUsersAsync()
    {
        var faker = new Faker<User>()
            .RuleFor(u => u.Name, fa => fa.Name.FirstName())
            .RuleFor(u => u.UserName, fa => fa.Internet.UserName())
            .RuleFor(u => u.Email, fa => fa.Internet.Email());
        
        var users = faker.Generate(20);
        var rnd = new Random();
        foreach (var user in users)
        {
            var userResult = await identity.CreateUserAsync(user, "123456");
            if (userResult.Succeeded)
            {
                var rol = rnd.Next(0, 1);
                if (rol == 1)
                    await identity.AddToRoleAsync(user, UserRoles.Admin);
                else 
                    await identity.AddToRoleAsync(user, UserRoles.Customer);
            }
        }
    }

}