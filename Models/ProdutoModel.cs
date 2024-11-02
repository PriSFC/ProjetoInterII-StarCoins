namespace StarCoins.Models;

public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Moeda { get; set; }
        public int Quantidade { get; set; }
        public int Status { get; set; } // 1 - em estoque, 2 - esgotado, 3 - descontinuado, 4 - em trânsito

        // Método abstrato, deve ser implementado nas classes derivadas
        //public abstract void ExibirDetalhes();
    }