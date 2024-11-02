namespace StarCoins.Models;

public class ProdutoFisico : Produto, IProduto
    {
        public float Peso { get; set; } // Exemplo de propriedade específica de um produto físico

        public override void ExibirDetalhes()
         {
            // Lógica específica para exibir detalhes de Produto Físico
            Console.WriteLine($"Produto Físico: {Nome}, Peso: {Peso} gramas");
         }
    }