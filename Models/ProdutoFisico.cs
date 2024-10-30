namespace StarCoins.Models;

public class ProdutoFisico : Produto
    {
        public decimal Peso { get; set; } // Exemplo de propriedade específica de um produto físico

        public override void ExibirDetalhes()
        {
        }
    }