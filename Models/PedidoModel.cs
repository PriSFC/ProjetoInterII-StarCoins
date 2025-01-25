using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarCoins.Models;

public class Pedido {
    
    public int PedidoId { get; set; }
    public int UsuarioId { get; set; }
    public int ProdutoId { get; set; }

    public DateOnly DataPedido {get; set; }
    public int Moeda { get; set; }
    public string? Ticket { get; set; }
    public string Status { get; set; } //(1 - em andamento, 2 - concluído)

    // Propriedades de navegação
    public virtual Usuario Usuario { get; set; }
    public virtual Produto Produto { get; set; }

}