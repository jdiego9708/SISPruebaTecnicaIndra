using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using IndraEstudiantes.Entidades.Herramientas.Interfaces;
using IndraEstudiantes.Entidades.Herramientas;

namespace IndraEstudiantes.Cliente.Pages
{
    public class ObservarCalificacionesPageModel : PageModel
    {
        private readonly IRestHelper IRestHelper;
        public ObservarCalificacionesPageModel(IRestHelper IRestHelper)
        {
            this.IRestHelper = IRestHelper;

            this.Calificaciones = new();
        }
        public void OnGet(int id)
        {
            this.Id_estudiante = id;
            this.ObtenerCalificaciones(id);
        }
        private void ObtenerCalificaciones(int id_estudiante)
        {
            try
            {
                BusquedaBindingModel busqueda = new()
                {
                    Tipo_busqueda = "ID ESTUDIANTE",
                    Texto_busqueda = id_estudiante.ToString(),
                };

                RestResponseModel response = this.IRestHelper.CallMethodPost("/BuscarCalificaciones",
                    JsonConvert.SerializeObject(busqueda));

                if (response == null)
                    throw new Exception("Error buscando las calificaciones");

                if (!response.IsSucess)
                    throw new Exception($"Error buscando las calificaciones | {response.Response}");

                JToken jtoken = JToken.Parse(response.Response);

                string infoJson = jtoken.ToString();

                if (string.IsNullOrEmpty(infoJson))
                    throw new Exception($"Error buscando las calificaciones");

                this.Calificaciones = JsonConvert.DeserializeObject<List<Calificaciones>>(infoJson);
            }
            catch (Exception)
            {
                //Se puede controlar los errores y enviar una pagina con las alertas
            }
        }
        public List<Calificaciones> Calificaciones { get; set; }
        public int Id_estudiante { get; set; }
    }
}
