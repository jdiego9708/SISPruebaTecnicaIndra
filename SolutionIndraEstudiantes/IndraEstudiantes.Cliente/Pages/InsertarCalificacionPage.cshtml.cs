using IndraEstudiantes.Entidades.Herramientas;
using IndraEstudiantes.Entidades.Modelos;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using IndraEstudiantes.Entidades.Herramientas.Interfaces;
using System.Collections.Generic;

namespace IndraEstudiantes.Cliente.Pages
{
    public class InsertarCalificacionPageModel : PageModel
    {
        private readonly IRestHelper IRestHelper;
        public InsertarCalificacionPageModel(IRestHelper IRestHelper)
        {
            this.IRestHelper = IRestHelper;

            this.Nombre_estudiante = string.Empty;
        }
        public void OnGet(int id)
        {
           this.Id_estudiante = id;

            try
            {
                BusquedaBindingModel busqueda = new()
                {
                    Tipo_busqueda = "ID ESTUDIANTE",
                    Texto_busqueda = id.ToString(),
                };

                RestResponseModel response = this.IRestHelper.CallMethodPost("/BuscarEstudiantes",
                    JsonConvert.SerializeObject(busqueda));

                if (response == null)
                    throw new Exception("Error buscando el estudiante");

                if (!response.IsSucess)
                    throw new Exception($"Error buscando el estudiante | {response.Response}");

                JToken jtoken = JToken.Parse(response.Response);

                string infoJson = jtoken.ToString();

                if (string.IsNullOrEmpty(infoJson))
                    throw new Exception($"Error buscando el estudiante");

                List < Estudiantes > estudiantes = 
                    JsonConvert.DeserializeObject<List<Estudiantes>>(infoJson);

                if (estudiantes != null)
                {
                    this.Nombre_estudiante = 
                        estudiantes[0].Nombres_estudiante + " " + estudiantes[0].Apellidos_estudiante;
                }
            }
            catch (Exception)
            {
                //Se puede controlar los errores y enviar una pagina con las alertas
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                var materia = Request.Form["txtMateria"];
                var periodo = Request.Form["txtPeriodo"];
                var nota = Request.Form["txtNota"];
                var id = Request.Form["txtIdEstudiante"];

                Calificaciones calificacion = new()
                {
                    Materia = ConvertValueHelper.ConvertirCadena(materia),
                    Periodo = ConvertValueHelper.ConvertirCadena(periodo),
                    Valor_nota = ConvertValueHelper.ConvertirDecimal(nota),
                    Id_estudiante = ConvertValueHelper.ConvertirNumero(id),
                };

                RestResponseModel response = this.IRestHelper.CallMethodPost("/InsertarCalificacion",
                    JsonConvert.SerializeObject(calificacion));

                if (response == null)
                    throw new Exception();

                if (!response.IsSucess)
                    throw new Exception();

                JToken jtoken = JToken.Parse(response.Response);

                Calificaciones calificacionResponse = JsonConvert.DeserializeObject<Calificaciones>(jtoken.ToString());

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/NotFound");
            }
        }

        public int Id_estudiante { get; set; }
        public string Nombre_estudiante { get; set; }
    }
}
