
namespace Application.Dtos.Auth
{
    public class IniciarSesionResponse
    {
        public bool valido { get; set; }
        public string idUsuario { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
        public string mensaje { get; set; }
        public int tiempoSession { get; set; }
    }
}
