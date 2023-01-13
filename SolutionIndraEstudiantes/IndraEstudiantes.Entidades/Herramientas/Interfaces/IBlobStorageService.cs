using IndraEstudiantes.Entidades.ModelosConfiguracion;

namespace IndraEstudiantes.Entidades.Herramientas.Interfaces
{
    public interface IBlobStorageService
    {
        BlobResponse DescargarArchivoContainerBlobStorage(string nombreArchivo, string contenedor);
        BlobResponse SubirArchivoContainerBlobStorage(Stream inputStream, string nombreArchivo, string contenedor, string contentType = "application/json");
    }
}
