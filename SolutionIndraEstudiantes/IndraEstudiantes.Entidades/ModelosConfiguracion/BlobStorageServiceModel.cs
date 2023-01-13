namespace IndraEstudiantes.Entidades.ModelosConfiguracion
{
    public class BlobStorageServiceModel
    {
        public string DefaultEndpointsProtocol { get; set; }
        public string ContainerNameRecursos { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string EndpointSuffix { get; set; }
    }
}
