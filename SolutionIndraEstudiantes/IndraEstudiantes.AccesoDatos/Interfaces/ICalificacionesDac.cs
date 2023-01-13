using IndraEstudiantes.Entidades.Modelos;
using System.Data;

namespace IndraEstudiantes.AccesoDatos.Interfaces
{
    public interface ICalificacionesDac
    {
        string InsertarCalificacion(Calificaciones calificacion);
        string EditarCalificacion(Calificaciones calificacion);
        string BuscarCalificaciones(string tipo_busqueda, string texto_busqueda,
            out DataTable dtCalificaciones);
    }
}
