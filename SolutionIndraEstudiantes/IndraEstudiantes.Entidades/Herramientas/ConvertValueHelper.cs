using Newtonsoft.Json.Linq;

namespace IndraEstudiantes.Entidades.Herramientas
{
    /// <summary>
    /// Clase de ayuda para convertir valores y controlar las posibles referencias nulas que que surgan
    /// Se puede mejorar implementando interfaces e inyección de dependencias
    /// </summary>
    public class ConvertValueHelper
    {
        public static bool ComprobarJsonFormat(string json)
        {
            try
            {
                JObject obj = JObject.Parse(json);
                return true;
            }
            catch (Exception)
            {
                
            }

            try
            {
                JToken obj = JToken.Parse(json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string ConvertirCadena(object obj)
        {
            try
            {
                if (obj == null)
                    return string.Empty;

                return Convert.ToString(obj);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static int ConvertirNumero(object obj)
        {
            try
            {
                return Convert.ToInt32(Convert.ToString(obj));
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal ConvertirDecimal(object obj)
        {
            try
            {
                if (obj == null)
                    return 0;

                string value = Convert.ToString(obj);
                string separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                if (value == null)
                    return 0;

                if (!value.Contains(separator))
                {
                    if (separator.Equals(","))
                        value = value.Replace(".", ",");
                    else
                        value = value.Replace(",", ".");
                }

                if (decimal.TryParse(value, out decimal result))
                {
                    return decimal.Round(result, 2, MidpointRounding.AwayFromZero);
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static double ConvertirDouble(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static DateTime ConvertirFecha(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch (Exception)
            {
                return new DateTime(01, 01, 01);
            }
        }
        public static bool ConvertirBoolean(object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static TimeSpan ConvertirHora(object obj)
        {
            TimeSpan tiempo = new(0, 0, 0);
            try
            {
                string hora = Convert.ToString(obj);
                if (hora.Contains('.'))
                {
                    int index = hora.IndexOf(".");
                    if (index > 0)
                    {
                        string horaeditada = hora[..index];
                        hora = horaeditada;
                    }
                }

                int[] partes = hora.Split(new char[] { ':' }).Select(x => Convert.ToInt32(x)).ToArray();
                if (partes.Length == 2)
                {
                    tiempo = new TimeSpan(partes[0], partes[1], 0);
                }
                else
                {
                    tiempo = new TimeSpan(partes[0], partes[1], partes[2]);
                }
            }
            catch (Exception)
            {

            }

            return tiempo;
        }
        public static string TimeSpanToString(TimeSpan hora, string format)
        {
            string tiempo;
            DateTime date = new DateTime().Add(hora);
            tiempo = date.ToString(format);

            return tiempo;
        }
    }
}
