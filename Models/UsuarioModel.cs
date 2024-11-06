using System.ComponentModel.DataAnnotations;

namespace StarCoins.Models {
    public class Usuario {
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Perfil é obrigatório")]
        public string Perfil { get; set; } // Adm, Prof, Aluno
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login {  get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; } 
    }
}
