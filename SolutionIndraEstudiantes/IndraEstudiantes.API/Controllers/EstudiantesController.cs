using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using IndraEstudiantes.Servicios.Interfaces;

namespace IndraEstudiantes.Controllers
{
    [ApiController]
    public class EstudiantesController : Controller
    {
        private readonly ILogger<EstudiantesController> logger;
        private IEstudiantesServicio IEstudiantesServicio { get; set; }
        public EstudiantesController(ILogger<EstudiantesController> logger,
            IEstudiantesServicio IEstudiantesServicio)
        {
            this.logger = logger;
            this.IEstudiantesServicio = IEstudiantesServicio;
        }

        [HttpPost]
        [Route("InsertarEstudiante")]
        public IActionResult InsertarEstudiante(JObject estudianteJson)
        {
            try
            {
                logger.LogInformation("Inicio de insertar estudiantes");

                if (estudianteJson == null)
                    throw new Exception("Insertar estudiante vacío compruebe la info enviada");

                Estudiantes estudiante = estudianteJson.ToObject<Estudiantes>();
                
                if (estudiante == null)
                {
                    logger.LogInformation("Sin información de estudiante");
                    throw new Exception("Sin información de estudiante");
                }
                else
                {
                    RestResponseModel rpta = this.IEstudiantesServicio.InsertarEstudiante(estudiante);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Creación de estudiante exitoso");
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
                logger.LogError("Error el controlador de insertar estudiante", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("EditarEstudiante")]
        public IActionResult EditarEstudiante(JObject estudianteJson)
        {
            try
            {
                logger.LogInformation("Inicio de editar estudiante");

                if (estudianteJson == null)
                    throw new Exception("Editar estudiante vacío compruebe la info enviada");

                Estudiantes estudiante = estudianteJson.ToObject<Estudiantes>();

                if (estudiante == null)
                {
                    logger.LogInformation("Sin información de estudiante");
                    throw new Exception("Sin información de estudiante");
                }
                else
                {
                    RestResponseModel rpta = this.IEstudiantesServicio.EditarEstudiante(estudiante);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Actualización de estudiante exitoso");
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
                logger.LogError("Error el controlador de editar estudiante", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarEstudiantes")]
        public IActionResult BuscarEstudiantes(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar estudiante");

                if (busquedaJson == null)
                    throw new Exception("Buscar estudiante vacío compruebe la info enviada");

                BusquedaBindingModel busqueda = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busqueda == null)
                {
                    logger.LogInformation("Sin información de busqueda estudiante");
                    throw new Exception("Sin información de busqueda estudiante");
                }
                else
                {
                    RestResponseModel rpta = this.IEstudiantesServicio.BuscarEstudiantes(busqueda);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Busqueda de estudiante exitoso");
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
                logger.LogError("Error en el controlador de buscar estudiante", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
