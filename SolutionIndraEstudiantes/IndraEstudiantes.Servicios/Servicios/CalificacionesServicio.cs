using IndraEstudiantes.AccesoDatos.Interfaces;
using IndraEstudiantes.Entidades.Herramientas.Interfaces;
using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using IndraEstudiantes.Servicios.Interfaces;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace IndraEstudiantes.Servicios.Servicios
{
    public class CalificacionesServicio : ICalificacionesServicio
    {
        #region CONSTRUCTORES E INYECCION DE DEPENDENCIAS
        public ICalificacionesDac ICalificacionesDac { get; set; }
        public IBlobStorageService IBlobStorageService { get; set; }
        public CalificacionesServicio(ICalificacionesDac ICalificacionesDac,
            IBlobStorageService IBlobStorageService)
        {
            this.ICalificacionesDac = ICalificacionesDac;
            this.IBlobStorageService = IBlobStorageService;
        }
        #endregion

        #region MÉTODOS
        private bool ComprobacionesInsertar(Calificaciones calificacion, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (calificacion.Id_estudiante == 0)
                    throw new Exception("El id de estudiante no puede ser 0");

                if (string.IsNullOrEmpty(calificacion.Materia))
                    throw new Exception("La Materia no puede estar vacío");

                if (string.IsNullOrEmpty(calificacion.Periodo))
                    throw new Exception("El Periodo no puede estar vacío");

                if (calificacion.Valor_nota == 0)
                    throw new Exception("El Valor_nota no puede ser 0");

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel InsertarCalificacin(Calificaciones calificacion)
        {
            RestResponseModel response = new();
            try
            {
                if (!this.ComprobacionesInsertar(calificacion, out string rpta))
                    throw new Exception(rpta);

                rpta = this.ICalificacionesDac.InsertarCalificacion(calificacion);

                if (!rpta.Equals("OK"))
                    throw new Exception($"No se insertó la calificación | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(calificacion);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        private bool ComprobacionesEditar(Calificaciones calificacion, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (calificacion.Id_calificacion == 0)
                    throw new Exception("El id de calificación no puede ser 0");

                if (calificacion.Id_estudiante == 0)
                    throw new Exception("El id de estudiante no puede ser 0");

                if (string.IsNullOrEmpty(calificacion.Materia))
                    throw new Exception("La Materia no puede estar vacío");

                if (string.IsNullOrEmpty(calificacion.Periodo))
                    throw new Exception("El Periodo no puede estar vacío");

                if (calificacion.Valor_nota == 0)
                    throw new Exception("El Valor_nota no puede ser 0");

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel EditarCalificacion(Calificaciones calificacion)
        {
            RestResponseModel response = new();
            try
            {
                if (!this.ComprobacionesEditar(calificacion, out string rpta))
                    throw new Exception(rpta);

                rpta = this.ICalificacionesDac.EditarCalificacion(calificacion);

                if (!rpta.Equals("OK"))
                    throw new Exception($"No se editó la calificación | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(calificacion);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel BuscarCalificacion(BusquedaBindingModel busqueda)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(busqueda.Tipo_busqueda))
                    throw new Exception("El tipo de búsqueda no puede estar vacío");

                if (string.IsNullOrEmpty(busqueda.Texto_busqueda))
                    throw new Exception("El texto de búsqueda no puede estar vacío");

                string rpta =
                    this.ICalificacionesDac.BuscarCalificaciones(busqueda.Tipo_busqueda,
                    busqueda.Texto_busqueda, out DataTable dtCalificacion);

                List<Calificaciones> calificaciones = new();

                if (dtCalificacion == null)
                {
                    if (rpta.Equals("OK"))
                    {
                        calificaciones.Add(new Calificaciones()
                        {
                            Materia = "NO HAY CALIFICACIONES PARA MOSTRAR",
                        });

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(calificaciones);
                        return response;
                    }
                    else
                        throw new Exception($"Error | {rpta}");
                }

                calificaciones = (from DataRow row in dtCalificacion.Rows
                            select new Calificaciones(row)).ToList();

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(calificaciones);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel GuardarArchivoCalificaciones(BusquedaBindingModel busqueda)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(busqueda.Tipo_busqueda))
                    throw new Exception("El tipo de búsqueda no puede estar vacío");

                if (string.IsNullOrEmpty(busqueda.Texto_busqueda))
                    throw new Exception("El texto de búsqueda no puede estar vacío");

                string rpta =
                    this.ICalificacionesDac.BuscarCalificaciones(busqueda.Tipo_busqueda,
                    busqueda.Texto_busqueda, out DataTable dtCalificacion);

                List<Calificaciones> calificaciones = new();

                if (dtCalificacion == null)
                {
                    if (rpta.Equals("OK"))
                    {
                        calificaciones.Add(new Calificaciones()
                        {
                            Materia = "NO HAY CALIFICACIONES PARA MOSTRAR",
                        });

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(calificaciones);
                        return response;
                    }
                    else
                        throw new Exception($"Error | {rpta}");
                }

                calificaciones = (from DataRow row in dtCalificacion.Rows
                                  select new Calificaciones(row)).ToList();

                string calificacionesJson = JsonConvert.SerializeObject(calificaciones);

                MemoryStream streamJson = new(Encoding.UTF8.GetBytes(calificacionesJson));

                BlobResponse blobresponse = this.IBlobStorageService.SubirArchivoContainerBlobStorage(streamJson,
                    $"pruebaindra{DateTime.Now:yyyy-MM-dd}{DateTime.Now:HH_mm}.json", "archivosvarios");

                response.IsSucess = true;
                response.Response = Convert.ToString(blobresponse.Message);
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
