namespace BarberShopAPI.ModelsDTO
{
    public class CitaDTO
    {

        public int Citaid { get; set; }
        public string? Descripcion { get; set; }
        public DateTime Fechad { get; set; }
        public string? Horad { get; set; }
        public bool? Actived { get; set; }
        public int IdUser { get; set; }
        public int IdCliente { get; set; }
        public int IdCategoriaCita { get; set; }

        public string NombreDeCliente { get; set; } = null!;
        public string ApellidosDeCliente { get; set; } = null!;
        public string? CorreoDeCliente { get; set; }
        public string? CelularDeCliente { get; set; }
    }
}
