using System.ComponentModel.DataAnnotations;

namespace StarCoins.Models;

public abstract class Produto : IProduto
    {
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Moeda é obrigatória")]
        public int Moeda { get; set; }
        [Required(ErrorMessage = "Quantidade é obrigatória")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "Status é obrigatório")]
        public int Status { get; set; } // 1 - em estoque, 2 - esgotado, 3 - descontinuado, 4 - em trânsito

        // Método abstrato, deve ser implementado nas classes derivadas
        public abstract void ExibirDetalhes();
    }