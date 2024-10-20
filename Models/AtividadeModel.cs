using System.ComponentModel.DataAnnotations;

namespace StarCoins.Models;
public class Atividade {
    public int AtividadeId { get; set; }
    public string Nome { get; set; }
    public string Descricao {get; set; }
    public double Moeda {get; set; }
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateOnly DataEntrega { get; set; } 

    public bool IsFinalized { get; set; }

}