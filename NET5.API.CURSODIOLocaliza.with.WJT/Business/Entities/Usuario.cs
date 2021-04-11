namespace NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities
{
    /// <summary>
    /// ENTIDADE DE BANCO DE DADOS
    /// </summary>
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
