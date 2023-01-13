using IndraEstudiantes.Entidades.Herramientas;
using System.Data;

namespace IndraEstudiantes.Entidades.Modelos
{
    public class Calificaciones
    {
        #region CONSTRUCTORES
        public Calificaciones()
        {
            this.Materia = string.Empty;
            this.Periodo = string.Empty;

            this.Estudiante = new();
        }
        public Calificaciones(DataRow row)
        {
            this.Id_calificacion = ConvertValueHelper.ConvertirNumero(row["Id_calificacion"]);
            this.Id_estudiante = ConvertValueHelper.ConvertirNumero(row["Id_estudiante"]);
            this.Materia = ConvertValueHelper.ConvertirCadena(row["Materia"]);
            this.Periodo = ConvertValueHelper.ConvertirCadena(row["Periodo"]);
            this.Valor_nota = ConvertValueHelper.ConvertirDecimal(row["Valor_nota"]);

            if (row.Table.Columns.Contains("Nombres_estudiante"))
                this.Estudiante = new(row);
            else
                this.Estudiante = new();
        }
        #endregion

        public int Id_calificacion { get; set; }
        public int Id_estudiante { get; set; }
        public Estudiantes Estudiante { get; set; }
        public string Materia { get; set; }
        public string Periodo { get; set; }
        public decimal Valor_nota { get; set; }
    }
}
