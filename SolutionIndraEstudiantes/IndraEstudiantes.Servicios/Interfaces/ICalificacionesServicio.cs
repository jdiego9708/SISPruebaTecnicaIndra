using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;

namespace IndraEstudiantes.Servicios.Interfaces
{
    public interface ICalificacionesServicio
    {
        RestResponseModel InsertarCalificacin(Calificaciones calificacion);
        RestResponseModel EditarCalificacion(Calificaciones calificacion);
        RestResponseModel BuscarCalificacion(BusquedaBindingModel busqueda);
    }
}
