using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;

namespace IndraEstudiantes.Servicios.Interfaces
{
    public interface IEstudiantesServicio
    {
        RestResponseModel InsertarEstudiante(Estudiantes estudiante);
        RestResponseModel EditarEstudiante(Estudiantes estudiante);
        RestResponseModel BuscarEstudiantes(BusquedaBindingModel busqueda);
    }
}
