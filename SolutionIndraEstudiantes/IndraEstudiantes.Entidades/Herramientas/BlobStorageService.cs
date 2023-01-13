using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Blobs.Models;
using IndraEstudiantes.Entidades.ModelosConfiguracion;
using IndraEstudiantes.Entidades.Herramientas.Interfaces;
using Microsoft.Extensions.Configuration;

namespace IndraEstudiantes.Entidades.Herramientas
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobStorageServiceModel BlobStorageServiceModel;
        private readonly IConfiguration Configuration;
        public BlobStorageService(IConfiguration IConfiguration)
        {
            this.Configuration = IConfiguration;

            var settings = this.Configuration.GetSection("BlobStorageService");
            this.BlobStorageServiceModel = settings.Get<BlobStorageServiceModel>();
        }
        private BlobContainerClient ConfiguracionContenedor(string nombreContenedor)
        {
            string defaultEndpointsProtocol = this.BlobStorageServiceModel.DefaultEndpointsProtocol;
            string accountName = this.BlobStorageServiceModel.AccountName;
            string accountKey = this.BlobStorageServiceModel.AccountKey;
            string endPointSuffix = this.BlobStorageServiceModel.EndpointSuffix;
            string connectionString = $"DefaultEndpointsProtocol={defaultEndpointsProtocol};AccountName={accountName};AccountKey={accountKey};EndpointSuffix={endPointSuffix}";

            return new BlobContainerClient(connectionString, nombreContenedor);
        }
        public BlobResponse DescargarArchivoContainerBlobStorage(string nombreArchivo, string contenedor)
        {
            BlobResponse blobResponse = new();
            try
            {
                BlobContainerClient clientContainer = ConfiguracionContenedor(contenedor);
                clientContainer.CreateIfNotExists(PublicAccessType.Blob);

                BlockBlobClient blockBlob = clientContainer.GetBlockBlobClient(nombreArchivo);
                Stream mem = new MemoryStream();
                if (blockBlob != null)
                {
                    blockBlob.DownloadTo(mem);
                }
                byte[] archivo = ((MemoryStream)mem).ToArray();
                blobResponse.IsSuccess = true;
                blobResponse.Message = archivo;
            }
            catch (Exception ex)
            {
                blobResponse.IsSuccess = false;
                blobResponse.Message = ex.Message;
            }
            return blobResponse;
        }
        public BlobResponse SubirArchivoContainerBlobStorage(Stream inputStream, string nombreArchivo, string contenedor, string contentType = "application/json")
        {
            BlobResponse blobResponse = new();
            try
            {
                BlobContainerClient clientContainer = ConfiguracionContenedor(contenedor);
                clientContainer.CreateIfNotExists(PublicAccessType.Blob);

                BlockBlobClient blockBlob = clientContainer.GetBlockBlobClient(nombreArchivo);

                BlobHttpHeaders headers = new() { ContentType = contentType };

                blockBlob.Upload(inputStream, new BlobUploadOptions { HttpHeaders = headers });

                blobResponse.IsSuccess = true;
                blobResponse.Message = blockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                blobResponse.IsSuccess = false;
                blobResponse.Message = ex.Message;
            }
            return blobResponse;
        }
    }
}
