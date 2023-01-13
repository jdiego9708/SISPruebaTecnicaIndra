using IndraEstudiantes.Entidades.ModelosConfiguracion;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using IndraEstudiantes.Entidades.Herramientas.Interfaces;

namespace IndraEstudiantes.Entidades.Herramientas
{
    public class RestHelper : IRestHelper
    {
        public IConfiguration Configuration { get; set; }
        private readonly ConfiguracionIndraAPI ConfiguracionIndraAPI;
        public RestHelper(IConfiguration Configuration)
        {
            this.Configuration = Configuration;

            var settings = this.Configuration.GetSection("ConfiguracionIndraAPI");
            this.ConfiguracionIndraAPI = settings.Get<ConfiguracionIndraAPI>();
        }
        public RestResponseModel CallMethodPost(string controller, string data)
        {
            try
            {
                string apiUrl;
                apiUrl = this.ConfiguracionIndraAPI.URLApiDesarrollo;

                string url = $"{apiUrl}{controller}";

                RestClient client = new(url);
                RestRequest request = new()
                {
                    Method = Method.Post
                };

                request.AddHeader("Content-Type", "application/json");

                request.AddJsonBody(data);

                RestResponse result = client.Execute(request);

                if (result == null)
                    throw new Exception("Error llamando al servidor");

                string content = result.Content.ToString();

                if (string.IsNullOrEmpty(content))
                    throw new Exception("Error con el contenido de la respuesta");

                if (!result.IsSuccessful)
                    throw new Exception(content);

                if (result.IsSuccessful)
                {
                    return new RestResponseModel
                    {
                        IsSucess = true,
                        Response = result.Content,
                    };
                }
                else
                {
                    return new RestResponseModel
                    {
                        IsSucess = false,
                        Response = result.Content,
                    };
                }
            }
            catch (Exception ex)
            {
                return new RestResponseModel
                {
                    IsSucess = false,
                    Response = ex.Message,
                };
            }
        }
        public bool ValidateJSON(string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }
    }
}
