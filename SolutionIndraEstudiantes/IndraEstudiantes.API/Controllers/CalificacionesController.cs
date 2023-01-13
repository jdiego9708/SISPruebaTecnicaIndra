using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using IndraEstudiantes.Servicios.Interfaces;

namespace IndraEstudiantes.Controllers
{
    [ApiController]
    public class CalificacionesController : Controller
    {
        private readonly ILogger<CalificacionesController> logger;
        private ICalificacionesServicio ICalificacionesServicio { get; set; }
        public CalificacionesController(ILogger<CalificacionesController> logger,
            ICalificacionesServicio ICalificacionesServicio)
        {
            this.logger = logger;
            this.ICalificacionesServicio = ICalificacionesServicio;
        }

        [HttpPost]
        [Route("InsertarCalificacion")]
        public IActionResult InsertarCalificacion(JObject calificacionJson)
        {
            try
            {
                logger.LogInformation("Inicio de insertar calificaciones");

                if (calificacionJson == null)
                    throw new Exception("Insertar calificacion vacío compruebe la info enviada");

                Calificaciones calificacion = calificacionJson.ToObject<Calificaciones>();
                
                if (calificacion == null)
                {
                    logger.LogInformation("Sin información de calificacion");
                    throw new Exception("Sin información de calificacion");
                }
                else
                {
                    RestResponseModel rpta = this.ICalificacionesServicio.InsertarCalificacin(calificacion);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Creación de calificacion exitoso");
                        return Ok(rpta.Response);
                    }
                    else
                    {
                        return BadRequest(rpta.Response);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error el controlador de insertar calificacion", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("EditarCalificacion")]
        public IActionResult EditarCalificacion(JObject calificacionJson)
        {
            try
            {
                logger.LogInformation("Inicio de editar calificacion");

                if (calificacionJson == null)
                    throw new Exception("Editar calificacion vacío compruebe la info enviada");

                Calificaciones calificacion = calificacionJson.ToObject<Calificaciones>();

                if (calificacion == null)
                {
                    logger.LogInformation("Sin información de calificacion");
                    throw new Exception("Sin información de calificacion");
                }
                else
                {
                    RestResponseModel rpta = this.ICalificacionesServicio.EditarCalificacion(calificacion);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Actualización de calificacion exitoso");
                        return Ok(rpta.Response);
                    }
                    else
                    {
                        return BadRequest(rpta.Response);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error el controlador de editar calificacion", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarCalificaciones")]
        public IActionResult BuscarCalificaciones(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar calificacion");

                if (busquedaJson == null)
                    throw new Exception("Buscar calificacion vacío compruebe la info enviada");

                BusquedaBindingModel busqueda = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busqueda == null)
                {
                    logger.LogInformation("Sin información de busqueda calificacion");
                    throw new Exception("Sin información de busqueda calificacion");
                }
                else
                {
                    RestResponseModel rpta = this.ICalificacionesServicio.BuscarCalificacion(busqueda);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Busqueda de calificacion exitoso");
                        return Ok(rpta.Response);
                    }
                    else
                    {
                        return BadRequest(rpta.Response);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error en el controlador de buscar calificacion", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
