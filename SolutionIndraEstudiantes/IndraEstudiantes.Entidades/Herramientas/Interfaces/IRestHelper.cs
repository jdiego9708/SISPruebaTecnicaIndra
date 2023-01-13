using IndraEstudiantes.Entidades.ModelosConfiguracion;

namespace IndraEstudiantes.Entidades.Herramientas.Interfaces
{
    public interface IRestHelper
    {
        RestResponseModel CallMethodPost(string controller, string data);
    }
}
