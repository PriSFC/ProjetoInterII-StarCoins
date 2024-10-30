namespace StarCoins.Models;

 public class ProdutoDigital : Produto
    {
        public double TamanhoArquivo { get; set; } // Exemplo de propriedade específica de um produto digital

        public override void ExibirDetalhes()
        {
        }
    }