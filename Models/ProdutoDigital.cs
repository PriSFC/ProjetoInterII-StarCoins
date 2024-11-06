namespace StarCoins.Models;

 public class ProdutoDigital : Produto, IProduto
    {
      
        public float TamanhoArquivo { get; set; } // Propriedade específica de um produto digital

         public override void ExibirDetalhes()
         {
            // Lógica específica para exibir detalhes de Produto Digital
            Console.WriteLine($"Produto Digital: {Nome}, Tamanho do Arquivo: {TamanhoArquivo} MB");
         }
    }