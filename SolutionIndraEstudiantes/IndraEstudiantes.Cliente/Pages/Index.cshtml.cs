using IndraEstudiantes.Entidades.Herramientas.Interfaces;
using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using IndraEstudiantes.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IndraEstudiantes.Cliente.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRestHelper IRestHelper;

        public IndexModel(ILogger<IndexModel> logger,
            IRestHelper IRestHelper)
        {
            _logger = logger;
            this.IRestHelper = IRestHelper;
            this.Estudiantes = new();
        }

        public List<Estudiantes> Estudiantes { get; set; }
        public void OnGet()
        {
            this.ObtenerEstudiantes();
        }
        private void ObtenerEstudiantes()
        {
            try
            {
                BusquedaBindingModel busqueda = new()
                {
                    Tipo_busqueda = "TODOS",
                    Texto_busqueda = "TODOS"
                };

                RestResponseModel response = this.IRestHelper.CallMethodPost("/BuscarEstudiantes",
                    JsonConvert.SerializeObject(busqueda));

                if (response == null)
                    throw new Exception("Error buscando los estudiantes");

                if (!response.IsSucess)
                    throw new Exception($"Error buscando los estudiantes | {response.Response}");

                JToken jtoken = JToken.Parse(response.Response);

                string infoJson = jtoken.ToString();

                if (string.IsNullOrEmpty(infoJson))
                    throw new Exception($"Error buscando los estudiantes");

                this.Estudiantes = JsonConvert.DeserializeObject<List<Estudiantes>>(infoJson);
            }
            catch (Exception)
            {
                //Se puede controlar los errores y enviar una pagina con las alertas
            }
        }
    }
}