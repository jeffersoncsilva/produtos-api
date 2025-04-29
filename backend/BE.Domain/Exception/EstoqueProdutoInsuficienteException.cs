using BE.Domain.Entities;

namespace BE.Domain.Exception;

public class EstoqueProdutoInsuficienteException(Guid ProdutoId, string message) : System.Exception(message)
{
    
}