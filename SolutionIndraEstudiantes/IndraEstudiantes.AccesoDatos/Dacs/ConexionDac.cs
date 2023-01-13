using IndraEstudiantes.AccesoDatos.Interfaces;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using Microsoft.Extensions.Configuration;

namespace IndraEstudiantes.AccesoDatos.Dacs
{
    public class ConexionDac : IConexionDac
    {
        private readonly IConfiguration Configuration;
        private readonly ConnectionStrings ConnectionStringsModel;
        public ConexionDac(IConfiguration IConfiguration)
        {
            this.Configuration = IConfiguration;

            var settings = this.Configuration.GetSection("ConnectionStrings");
            this.ConnectionStringsModel = settings.Get<ConnectionStrings>();
        }
        public string Cn()
        {
            if (this.ConnectionStringsModel.ConexionBDPredeterminada.Equals("ConexionBDAzure"))
                return this.ConnectionStringsModel.ConexionBDAzure;
            else
                return this.ConnectionStringsModel.ConexionBDLocal;
        }
    }
}
