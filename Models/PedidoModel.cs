namespace StarCoins.Models;

public class Pedido {
    
    public int PedidoId { get; set; }
    public DateOnly DataPedido {get; set; }
    public double Moeda { get; set; }
    public int Ticket { get; set; }
    public string Status { get; set; } //(1 - em andamento, 2 - concluído)

    // Coleção de ProdutoPedido
    public ICollection<ProdutoPedido> ProdutoPedidos { get; set; }
}