using IndraEstudiantes.Entidades.Modelos;
using System.Data;

namespace IndraEstudiantes.AccesoDatos.Interfaces
{
    public interface IEstudiantesDac
    {
        string InsertarEstudiante(Estudiantes estudiante);
        string EditarEstudiante(Estudiantes estudiante);
        string BuscarEstudiantes(string tipo_busqueda, string texto_busqueda,
            out DataTable dtUsuarios);
    }
}
