using BE.Domain.Entities;
using BE.Persistence;
using Bogus;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS1998 
namespace BE.SeedDatabase;

public class SeedProducts(DefaultContext db)
{
    private static List<User>? Users = null;
    public async Task Seed(int qtd)
    {
        int salvo = 1;
        try
        {
            var faker = new Faker<Product>()
                .RuleFor(p => p.Name, fa => fa.Commerce.ProductName())
                .RuleFor(p => p.Description, fa => fa.Commerce.ProductDescription())
                .RuleFor(p => p.Price, fa => fa.Random.Decimal(1, 100_000M))
                .RuleFor(p => p.ImageUrl, fa => fa.Image.PicsumUrl())
                .RuleFor(p => p.Category, fa => fa.Commerce.Categories(1).FirstOrDefault())
                .RuleFor(p => p.Brand, fa => fa.Company.CompanyName())
                .RuleFor(p => p.Stock, fa => fa.Random.Int(1, 1000))
                .RuleFor(p => p.IsActive, fa => fa.Random.Bool(0.98f))
                .RuleFor(p => p.IsRemovedFromStock, fa => fa.Random.Bool(0.99f));
            faker.Locale = "pt_BR";
            var products = faker.Generate(qtd);
            int i = 0;
            foreach (Product p in products ?? [])
            {
                try
                {
                    string createdBy = await GetCreatedBy();
                    p.CreatedBy = createdBy;
                    p.ModifiedBy = createdBy;
                    await db.Products.AddAsync(p);
                    i++;
                    if (i == 1000)
                    {
                        salvo += 1000;
                        await db.SaveChangesAsync();
                        i = 0;
                        Console.WriteLine($"Salvando parcialmente produtos. Salvos: {salvo} de {qtd}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu uma exceção: " + ex.Message);
                }
            }
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu uma exeção: " + ex.Message);
            Console.WriteLine($"Foi salvo um total de: {salvo} produtos. ({(qtd / salvo):F2} %)");
        }
    }

    public async Task<string> GetCreatedBy()
    {
        var rnd = new Random();
        if (Users is null)
        {
            Users = await db.Users.AsNoTracking().ToListAsync();    
        }

        var idx = rnd.Next(0, Users.Count);
        return Users[idx].UserName!;
    }
}