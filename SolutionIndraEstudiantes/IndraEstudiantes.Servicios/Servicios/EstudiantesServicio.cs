using IndraEstudiantes.AccesoDatos.Interfaces;
using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using IndraEstudiantes.Servicios.Interfaces;
using Newtonsoft.Json;
using System.Data;

namespace IndraEstudiantes.Servicios.Servicios
{
    public class EstudiantesServicio : IEstudiantesServicio
    {
        #region CONSTRUCTORES E INYECCION DE DEPENDENCIAS
        public IEstudiantesDac IEstudiantesDac { get; set; }
        public EstudiantesServicio(IEstudiantesDac IEstudiantesDac)
        {
            this.IEstudiantesDac = IEstudiantesDac;
        }
        #endregion

        #region MÉTODOS
        private bool ComprobacionesInsertar(Estudiantes estudiante, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (string.IsNullOrEmpty(estudiante.Nombres_estudiante))
                    throw new Exception("El nombre no puede estar vacío");

                if (string.IsNullOrEmpty(estudiante.Apellidos_estudiante))
                    throw new Exception("El apellido no puede estar vacío");

                if (string.IsNullOrEmpty(estudiante.Sexo_estudiante))
                    throw new Exception("El sexo no puede estar vacío");

                if (string.IsNullOrEmpty(estudiante.Curso_estudiante))
                    throw new Exception("El curso no puede estar vacío");

                if (string.IsNullOrEmpty(estudiante.Estado_estudiante))
                    estudiante.Estado_estudiante = "ACTIVO";

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel InsertarEstudiante(Estudiantes estudiante)
        {
            RestResponseModel response = new();
            try
            {
                if (!this.ComprobacionesInsertar(estudiante, out string rpta))
                    throw new Exception(rpta);

                rpta = this.IEstudiantesDac.InsertarEstudiante(estudiante);

                if (!rpta.Equals("OK"))
                    throw new Exception($"No se insertó el estudiante | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(estudiante);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        private bool ComprobacionesEditar(Estudiantes estudiante, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (estudiante.Id_estudiante == 0)
                    throw new Exception("Verifique el id del estudiante no puede ser 0");

                if (string.IsNullOrEmpty(estudiante.Nombres_estudiante))
                    throw new Exception("El nombre no puede estar vacío");

                if (string.IsNullOrEmpty(estudiante.Apellidos_estudiante))
                    throw new Exception("El apellido no puede estar vacío");

                if (string.IsNullOrEmpty(estudiante.Sexo_estudiante))
                    throw new Exception("El sexo no puede estar vacío");

                if (string.IsNullOrEmpty(estudiante.Curso_estudiante))
                    throw new Exception("El curso no puede estar vacío");

                if (string.IsNullOrEmpty(estudiante.Estado_estudiante))
                    estudiante.Estado_estudiante = "ACTIVO";

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel EditarEstudiante(Estudiantes estudiante)
        {
            RestResponseModel response = new();
            try
            {
                if (!this.ComprobacionesEditar(estudiante, out string rpta))
                    throw new Exception(rpta);

                rpta = this.IEstudiantesDac.EditarEstudiante(estudiante);

                if (!rpta.Equals("OK"))
                    throw new Exception($"No se editó el estudiante | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(estudiante);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel BuscarEstudiantes(BusquedaBindingModel busqueda)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(busqueda.Tipo_busqueda))
                    throw new Exception("El tipo de búsqueda no puede estar vacío");

                if (string.IsNullOrEmpty(busqueda.Texto_busqueda))
                    throw new Exception("El texto de búsqueda no puede estar vacío");

                string rpta =
                    this.IEstudiantesDac.BuscarEstudiantes(busqueda.Tipo_busqueda,
                    busqueda.Texto_busqueda, out DataTable dtEstudiantes);

                List<Estudiantes> estudiantes = new();

                if (dtEstudiantes == null)
                {
                    if (rpta.Equals("OK"))
                    {
                        estudiantes.Add(new Estudiantes()
                        {
                            Nombres_estudiante = "NO HAY ESTUDIANTES REGISTRADOS"
                        });

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(estudiantes);
                        return response;
                    }
                    else
                        throw new Exception($"Error | {rpta}");
                }

                estudiantes = (from DataRow row in dtEstudiantes.Rows
                            select new Estudiantes(row)).ToList();

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(estudiantes);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        #endregion
    }
}
