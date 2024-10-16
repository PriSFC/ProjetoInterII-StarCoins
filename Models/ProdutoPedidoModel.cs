using StarCoins.Models;

public class ProdutoPedido {
    
    public int ProdutoId { get; set; }
    public int PedidoId { get; set; }
    public int Quantidade { get; set; }
    public double Moeda { get; set; }

    public Produto Produto { get; set; } // Produto (classe) e Produto (objeto)
    public Pedido Pedido { get; set; }
}