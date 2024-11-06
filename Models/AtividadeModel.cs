using System.ComponentModel.DataAnnotations;

namespace StarCoins.Models;
public class Atividade {
    public int AtividadeId { get; set; }
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Descrição é obrigatória")]
    public string Descricao {get; set; }
    [Required(ErrorMessage = "Moeda é obrigatória")]
    public int Moeda {get; set; }
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    [Required(ErrorMessage = "Data é obrigatória")]
    public DateOnly DataEntrega { get; set; } 


}