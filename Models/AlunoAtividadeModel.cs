namespace StarCoins.Models;
public class AlunoAtividade {
    public int AlunoAtividadeId { get; set; }
    public int AtividadeId { get; set; }
    public int UsuarioId { get; set; }
    public DateOnly DataRealizacao { get; set; }
    public int Status { get; set; } //(1 - não entregue, 2 - entregue)
    public double Nota { get; set; }

    public Aluno Aluno { get; set; } 
    public Atividade Atividade { get; set; }
}