using IndraEstudiantes.Entidades.ModelosConfiguracion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using IndraEstudiantes.Entidades.Herramientas.Interfaces;
using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.Herramientas;

namespace IndraEstudiantes.Cliente.Pages
{
    public class InsertarEstudianteModel : PageModel
    {
        private readonly IRestHelper IRestHelper;
        public InsertarEstudianteModel(IRestHelper IRestHelper)
        {
            this.IRestHelper = IRestHelper;
        }    

        public IActionResult OnPost()
        {
            try
            {
                var nombre = Request.Form["txtNombres"];
                var apellidos = Request.Form["txtApellidos"];
                var genero = Request.Form["listGenero"];
                var edad = Request.Form["txtEdad"];
                var curso = Request.Form["txtCurso"];

                Estudiantes estudiante = new()
                {
                    Nombres_estudiante = ConvertValueHelper.ConvertirCadena(nombre),
                    Apellidos_estudiante = ConvertValueHelper.ConvertirCadena(apellidos),
                    Sexo_estudiante = ConvertValueHelper.ConvertirCadena(genero),
                    Edad_estudiante = ConvertValueHelper.ConvertirNumero(edad),
                    Curso_estudiante = ConvertValueHelper.ConvertirCadena(curso),
                };

                RestResponseModel response = this.IRestHelper.CallMethodPost("/InsertarEstudiante",
                    JsonConvert.SerializeObject(estudiante));

                if (response == null)
                    throw new Exception();

                if (!response.IsSucess)
                    throw new Exception();

                JToken jtoken = JToken.Parse(response.Response);

                Estudiantes estudianteResponse = JsonConvert.DeserializeObject<Estudiantes>(jtoken.ToString());

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/NotFound");
            }
        }
    }
}
