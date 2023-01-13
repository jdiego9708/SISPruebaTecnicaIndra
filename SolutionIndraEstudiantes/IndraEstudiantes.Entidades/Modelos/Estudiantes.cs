using IndraEstudiantes.Entidades.Herramientas;
using System.Data;

namespace IndraEstudiantes.Entidades.Modelos
{
    public class Estudiantes
    {
        #region CONSTRUCTORES
        public Estudiantes()
        {
            this.Nombres_estudiante = string.Empty;
            this.Apellidos_estudiante = string.Empty;
            this.Sexo_estudiante = string.Empty;
            this.Curso_estudiante = string.Empty;
            this.Estado_estudiante = string.Empty;
        }
        public Estudiantes(DataRow row)
        {
            this.Id_estudiante = ConvertValueHelper.ConvertirNumero(row["Id_estudiante"]);
            this.Nombres_estudiante = ConvertValueHelper.ConvertirCadena(row["Nombres_estudiante"]);
            this.Apellidos_estudiante = ConvertValueHelper.ConvertirCadena(row["Apellidos_estudiante"]);
            this.Edad_estudiante = ConvertValueHelper.ConvertirNumero(row["Edad_estudiante"]);
            this.Sexo_estudiante = ConvertValueHelper.ConvertirCadena(row["Sexo_estudiante"]);
            this.Curso_estudiante = ConvertValueHelper.ConvertirCadena(row["Curso_estudiante"]);
            this.Estado_estudiante = ConvertValueHelper.ConvertirCadena(row["Estado_estudiante"]);
        }
        #endregion

        public int Id_estudiante { get; set; }
        public string Nombres_estudiante { get; set; }
        public string Apellidos_estudiante { get; set; }
        public int Edad_estudiante { get; set; }
        public string Sexo_estudiante { get; set; }
        public string Curso_estudiante { get; set; }
        public string Estado_estudiante { get; set; }
    }
}
