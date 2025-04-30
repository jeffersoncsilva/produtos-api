using System.Data;
using BE.Domain.Entities;
using Bogus;
using Npgsql;

#pragma warning disable CS1998 

namespace BE.SeedDatabase;

public class SeedSales
{
    private readonly NpgsqlConnection _connection;
    private List<Guid> _productsIds;
    private static int venda = 0;
    public SeedSales()
    {
        _connection = new NpgsqlConnection("Server=localhost;port=5432;Database=rodevtest;User Id=postgres;Password=root;");
        _connection.Open();
        var cmd = _connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT \"Id\" FROM public.\"Products\"";
        _productsIds = new List<Guid>();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            _productsIds.Add(reader.GetGuid(0));
        }
    }
    
    public async Task Seed(int qtd)
    {
        
        int salvo = 1;
        try
        {
            NpgsqlTransaction transaction = await _connection.BeginTransactionAsync();
            var rnd = new Random();
            DateTime.TryParse("2000-01-01 08:00", out DateTime dataInicio);
            DateTime.TryParse("2005-05-05 14:04", out DateTime dataInicio2);
            DateTime.TryParse("2025-05-05 19:38", out DateTime dataFim);
            var faker = new Faker<Sale>()
                .RuleFor(s => s.Name, fa => fa.Finance.AccountName())
                .RuleFor(s => s.IsCanceled, fa => fa.Random.Bool(0.98f))
                .RuleFor(s => s.Observation, fa => fa.Lorem.Text())
                .RuleFor(s => s.Descount, fa => fa.Random.Decimal(0, 100M))
                .RuleFor(s => s.CreatedOn, fa => fa.Date.Between(dataInicio, dataFim))
                .RuleFor(s => s.ModifiedOn, fa => fa.Date.Between(dataInicio2,dataFim));
            
            faker.Locale = "pt_BR";
            var sales = faker.Generate(qtd);
            int count = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Iniciando inserção de vendas.");
            foreach (var sale in sales)
            {
                try
                {
                    int qtdItens = rnd.Next(1, 25);

                    var cmdSale = _connection.CreateCommand();
                    cmdSale.Transaction = transaction;
                    cmdSale.CommandType = CommandType.Text;
                    cmdSale.CommandText = @"
                    INSERT INTO ""Sales"" (
                        ""Id"", 
                        ""Observation"",
                        ""Price"",
                        ""Descount"", 
                        ""CreatedOn"",
                        ""ModifiedOn"",
                        ""IsCanceled"",
                        ""Name""
                    ) VALUES (
                        @id, 
                        @observation,
                        @price,
                        @descount,
                        @createdOn,
                        @modifiedOn,
                        @isCanceled,
                        @name
                    )";
                    cmdSale.Parameters.AddWithValue("id", sale.Id);
                    cmdSale.Parameters.AddWithValue("observation", sale.Observation);
                    cmdSale.Parameters.AddWithValue("price", 0.0M);
                    cmdSale.Parameters.AddWithValue("descount", sale.Descount);
                    cmdSale.Parameters.AddWithValue("createdOn", sale.CreatedOn);
                    cmdSale.Parameters.AddWithValue("modifiedOn", sale.ModifiedOn);
                    cmdSale.Parameters.AddWithValue("iscanceled", sale.IsCanceled);
                    cmdSale.Parameters.AddWithValue("name", sale.Name);

                    await cmdSale.ExecuteNonQueryAsync();
                    
                    await SetRandomSaleItens(qtdItens, sale.Id, rnd, transaction);
                    
                    venda++;
                    count++;

                    if (venda == _productsIds.Count)
                    {
                        venda = 0;
                        _productsIds = _productsIds.OrderBy(x => rnd.Next()).ToList();
                    }
                    
                    if (count == 1000)
                    {
                        await transaction.CommitAsync();
                        await transaction.DisposeAsync();
                        watch.Stop();
                        transaction = await _connection.BeginTransactionAsync();
                        salvo += 1000;
                        count = 0;
                        Console.WriteLine($"Salvando parcialmente vendas. Salvo um total de {salvo} vendas de {qtd}.");
                        Console.WriteLine($"Foi gasto: {watch.Elapsed.Seconds} ms para salvar {salvo} vendas.");
                        watch.Restart();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir venda no banco. " + e.Message);
                }
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu uma exeção: " + ex.Message);
            Console.WriteLine($"Foi salvo um total de: {salvo} produtos. ({(qtd / salvo):F2} %)");
        }
    }
    
    private async Task SetRandomSaleItens(int qtd, Guid saleId, Random rnd, NpgsqlTransaction transaction)
    {
        var products = await GetRandomProducts(qtd);
        foreach (var product in products)
        {
            if(product.Stock <= 0)
                continue;
            
            int qtdProduct = rnd.Next(1, product.Stock);
            product.Stock -= qtdProduct;
            decimal price = product.Price * qtdProduct;
            
            await using var insertSaleItemCmd = _connection.CreateCommand();
            insertSaleItemCmd.Transaction = transaction;
            insertSaleItemCmd.CommandType = CommandType.Text;
            insertSaleItemCmd.CommandText = @"
            INSERT INTO ""SaleItens"" (
                ""Id"",
                ""ProductId"", 
                ""SaleId"", 
                ""Quantity"",
                ""Price"",
                ""CreatedOn"",
                ""ModifiedOn""
            ) VALUES (
                @id,
                @productId, 
                @saleId, 
                @quantity, 
                @price,
                @createdOn,
                @modifiedOn
            )";
            
            insertSaleItemCmd.Parameters.AddWithValue("id", Guid.NewGuid());
            insertSaleItemCmd.Parameters.AddWithValue("productId", product.Id);
            insertSaleItemCmd.Parameters.AddWithValue("saleId", saleId);
            insertSaleItemCmd.Parameters.AddWithValue("quantity", qtdProduct);
            insertSaleItemCmd.Parameters.AddWithValue("price", price);
            insertSaleItemCmd.Parameters.AddWithValue("createdOn", DateTime.Now.AddDays(-rnd.Next(5, 2000)));
            insertSaleItemCmd.Parameters.AddWithValue("modifiedOn", DateTime.Now.AddDays(-rnd.Next(2, 500)));

            await insertSaleItemCmd.ExecuteNonQueryAsync();

            await using var updateProductCmd = _connection.CreateCommand();
            updateProductCmd.CommandType = CommandType.Text;
            updateProductCmd.CommandText = "UPDATE public.\"Products\" SET \"Stock\" = @stock WHERE \"Id\" = @id";
            updateProductCmd.Parameters.AddWithValue("stock", product.Stock);
            updateProductCmd.Parameters.AddWithValue("id", product.Id);
            
            await updateProductCmd.ExecuteNonQueryAsync();
        }
    }
    
    private async Task<List<Produto>> GetRandomProducts(int qtd)
    {
        int skip = (qtd * venda > _productsIds.Count) ? venda : qtd * venda;
        var ids = _productsIds.Skip(skip).Take(qtd).ToList();
        
        string sql = "SELECT \"Id\", \"Price\", \"Stock\" FROM public.\"Products\" WHERE \"Id\" = ANY(@ids)";
        var cmd = _connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("ids", ids);

        await using var reader = await cmd.ExecuteReaderAsync();
        var products = new List<Produto>();
        while (await reader.ReadAsync())
        {
            var product = new Produto
            {
                Id = reader.GetGuid(0),
                Price = reader.GetDecimal(1),
                Stock = reader.GetInt32(2)
            };
            products.Add(product);
        }
        await reader.CloseAsync();
        return products;
    }
}

public class Produto
{
    public Guid Id { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; set; }
}