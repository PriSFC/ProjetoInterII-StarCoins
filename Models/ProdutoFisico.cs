using System.ComponentModel.DataAnnotations;

namespace StarCoins.Models;

public class ProdutoFisico : Produto, IProduto
    {
        [Required(ErrorMessage = "Peso é obrigatório")]
        public float Peso { get; set; } // Propriedade específica de um produto físico

        public override void ExibirDetalhes()
         {
            // Lógica específica para exibir detalhes de Produto Físico
            Console.WriteLine($"Produto Físico: {Nome}, Peso: {Peso} gramas");
         }
    }