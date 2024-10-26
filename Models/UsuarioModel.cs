namespace StarCoins.Models {
    public class Usuario {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; } // Adm, Prof, Aluno
        public string Login {  get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } 
    }
}
