using System.Data.SqlClient;
using System.Data;
using IndraEstudiantes.AccesoDatos.Interfaces;
using IndraEstudiantes.Entidades.Modelos;

namespace IndraEstudiantes.AccesoDatos.Dacs
{
    public class EstudiantesDac : IEstudiantesDac
    {
        #region CONSTRUCTOR E INYECCION DE DEPENDENCIAS
        private readonly IConexionDac Conexion;
        public EstudiantesDac(IConexionDac Conexion)
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

        #region MÉTODO INSERTAR ESTUDIANTE
        public string InsertarEstudiante(Estudiantes estudiante)
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
                    CommandText = "sp_Estudiantes_i",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_estudiante = new()
                {
                    ParameterName = "Id_estudiante",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_estudiante);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size

                #region PARÁMETROS

                SqlParameter Nombres_estudiante = new()
                {
                    ParameterName = "Nombres_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = estudiante.Nombres_estudiante,
                };
                SqlCmd.Parameters.Add(Nombres_estudiante);

                SqlParameter Apellidos_estudiante = new()
                {
                    ParameterName = "Apellidos_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = estudiante.Apellidos_estudiante,
                };
                SqlCmd.Parameters.Add(Apellidos_estudiante);

                SqlParameter Edad_estudiante = new()
                {
                    ParameterName = "Edad_estudiante",
                    SqlDbType = SqlDbType.Int,
                    Value = estudiante.Edad_estudiante,
                };
                SqlCmd.Parameters.Add(Edad_estudiante);

                SqlParameter Sexo_estudiante = new()
                {
                    ParameterName = "Sexo_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 2,
                    Value = estudiante.Sexo_estudiante,
                };
                SqlCmd.Parameters.Add(Sexo_estudiante);

                SqlParameter Curso_estudiante = new()
                {
                    ParameterName = "Curso_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 10,
                    Value = estudiante.Curso_estudiante,
                };
                SqlCmd.Parameters.Add(Curso_estudiante);

                SqlParameter Estado_estudiante = new()
                {
                    ParameterName = "Estado_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = estudiante.Estado_estudiante,
                };
                SqlCmd.Parameters.Add(Estado_estudiante);
                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
                //Obtenemos el id usuario y lo asignamos a la instancia existente de usuario para usarlo después
                estudiante.Id_estudiante = Convert.ToInt32(SqlCmd.Parameters["Id_estudiante"].Value);
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

        #region MÉTODO EDITAR USUARIO
        public string EditarEstudiante(Estudiantes estudiante)
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
                    CommandText = "sp_Estudiantes_i",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_usuario = new()
                {
                    ParameterName = "Id_estudiante",
                    SqlDbType = SqlDbType.Int,
                    Value = estudiante.Id_estudiante,
                };
                SqlCmd.Parameters.Add(Id_usuario);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size

                #region PARÁMETROS

                SqlParameter Nombres_estudiante = new()
                {
                    ParameterName = "Nombres_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = estudiante.Nombres_estudiante,
                };
                SqlCmd.Parameters.Add(Nombres_estudiante);

                SqlParameter Apellidos_estudiante = new()
                {
                    ParameterName = "Apellidos_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = estudiante.Apellidos_estudiante,
                };
                SqlCmd.Parameters.Add(Apellidos_estudiante);

                SqlParameter Edad_estudiante = new()
                {
                    ParameterName = "Edad_estudiante",
                    SqlDbType = SqlDbType.Int,
                    Value = estudiante.Edad_estudiante,
                };
                SqlCmd.Parameters.Add(Edad_estudiante);

                SqlParameter Sexo_estudiante = new()
                {
                    ParameterName = "Sexo_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 2,
                    Value = estudiante.Sexo_estudiante,
                };
                SqlCmd.Parameters.Add(Sexo_estudiante);

                SqlParameter Curso_estudiante = new()
                {
                    ParameterName = "Curso_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 10,
                    Value = estudiante.Curso_estudiante,
                };
                SqlCmd.Parameters.Add(Curso_estudiante);

                SqlParameter Estado_estudiante = new()
                {
                    ParameterName = "Estado_estudiante",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = estudiante.Estado_estudiante,
                };
                SqlCmd.Parameters.Add(Estado_estudiante);
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

        #region MÉTODO BUSCAR ESTUDIANTES
        public string BuscarEstudiantes(string tipo_busqueda, string texto_busqueda,
            out DataTable dtEstudiantes)
        {
            //Inicializamos la respuesta que vamos a devolver
            dtEstudiantes = new();
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
                    CommandText = "sp_Estudiantes_g",
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
                SqlData.Fill(dtEstudiantes);

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (dtEstudiantes == null)
                {
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
                }
                else
                {
                    if (dtEstudiantes.Rows.Count < 1)
                        dtEstudiantes = null;
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dtEstudiantes = null;
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
