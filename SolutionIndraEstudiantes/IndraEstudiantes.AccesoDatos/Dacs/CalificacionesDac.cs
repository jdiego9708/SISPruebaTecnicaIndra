using System.Data.SqlClient;
using System.Data;
using IndraEstudiantes.AccesoDatos.Interfaces;
using IndraEstudiantes.Entidades.Modelos;

namespace IndraEstudiantes.AccesoDatos.Dacs
{
    public class CalificacionesDac : ICalificacionesDac
    {
        #region CONSTRUCTOR E INYECCION DE DEPENDENCIAS
        private readonly IConexionDac Conexion;
        public CalificacionesDac(IConexionDac Conexion)
        {
            this.Mensaje_error = string.Empty;

            this.Conexion = Conexion;
        }
        #endregion

        #region MENSAJE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            string mensaje_error = e.Message;
            if (e.Errors != null)
            {
                if (e.Errors.Count > 0)
                {
                    mensaje_error += string.Join("|", e.Errors);
                }
            }
            this.Mensaje_error = mensaje_error;
        }
        #endregion

        #region PROPIEDADES
        public string Mensaje_error { get; set; }
        #endregion

        #region MÉTODO INSERTAR CALIFICACION
        public string InsertarCalificacion(Calificaciones calificacion)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 y > 11 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Calificaciones_i",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_calificacion = new()
                {
                    ParameterName = "Id_calificacion",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_calificacion);
                
                #region PARÁMETROS

                SqlParameter Id_estudiante = new()
                {
                    ParameterName = "Id_estudiante",
                    SqlDbType = SqlDbType.Int,
                    Value = calificacion.Id_estudiante,
                };
                SqlCmd.Parameters.Add(Id_estudiante);

                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Materia = new()
                {
                    ParameterName = "Materia",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = calificacion.Materia,
                };
                SqlCmd.Parameters.Add(Materia);

                SqlParameter Periodo = new()
                {
                    ParameterName = "Periodo",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = calificacion.Periodo,
                };
                SqlCmd.Parameters.Add(Periodo);

                SqlParameter Valor_nota = new()
                {
                    ParameterName = "Valor_nota",
                    SqlDbType = SqlDbType.Decimal,
                    Value = calificacion.Valor_nota,
                };
                SqlCmd.Parameters.Add(Valor_nota);
                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
                //Obtenemos el id y lo asignamos a la instancia existente de usuario para usarlo después
                calificacion.Id_calificacion = Convert.ToInt32(SqlCmd.Parameters["Id_calificacion"].Value);
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region MÉTODO EDITAR CALIFICACION
        public string EditarCalificacion(Calificaciones calificacion)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 y > 11 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Calificaciones_i",
                    CommandType = CommandType.StoredProcedure
                };

                #region PARÁMETROS
                SqlParameter Id_calificacion = new()
                {
                    ParameterName = "Id_calificacion",
                    SqlDbType = SqlDbType.Int,
                    Value = calificacion.Id_calificacion,
                };
                SqlCmd.Parameters.Add(Id_calificacion);

                SqlParameter Id_estudiante = new()
                {
                    ParameterName = "Id_estudiante",
                    SqlDbType = SqlDbType.Int,
                    Value = calificacion.Id_estudiante,
                };
                SqlCmd.Parameters.Add(Id_estudiante);

                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Materia = new()
                {
                    ParameterName = "Materia",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = calificacion.Materia,
                };
                SqlCmd.Parameters.Add(Materia);

                SqlParameter Periodo = new()
                {
                    ParameterName = "Periodo",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = calificacion.Periodo,
                };
                SqlCmd.Parameters.Add(Periodo);

                SqlParameter Valor_nota = new()
                {
                    ParameterName = "Valor_nota",
                    SqlDbType = SqlDbType.Decimal,
                    Value = calificacion.Valor_nota,
                };
                SqlCmd.Parameters.Add(Valor_nota);
                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region MÉTODO BUSCAR CALIFICACIONES
        public string BuscarCalificaciones(string tipo_busqueda, string texto_busqueda,
            out DataTable dtCalificaciones)
        {
            //Inicializamos la respuesta que vamos a devolver
            dtCalificaciones = new();
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Calificaciones_g",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = tipo_busqueda
                };
                SqlCmd.Parameters.Add(Tipo_busqueda);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Texto_busqueda = new()
                {
                    ParameterName = "Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = texto_busqueda,
                };
                SqlCmd.Parameters.Add(Texto_busqueda);

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                SqlDataAdapter SqlData = new(SqlCmd);
                SqlData.Fill(dtCalificaciones);

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (dtCalificaciones == null)
                {
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
                }
                else
                {
                    if (dtCalificaciones.Rows.Count < 1)
                        dtCalificaciones = null;
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dtCalificaciones = null;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion
    }
}
