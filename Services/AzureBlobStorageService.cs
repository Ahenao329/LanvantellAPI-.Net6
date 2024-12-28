using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LavantellAPIS.Enums;

namespace LavantellAPIS.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly string azureStorageConnectionString;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            this.azureStorageConnectionString = configuration.GetValue<string>("AzureStorageConnectionString");
        }

        public async Task DeleteAsync(ContainerEnum container, string blobFilename)
        {
            var containerName = Enum.GetName(typeof(ContainerEnum), container).ToLower();//obtenemos el nombre del conteiner
            var blobContainerClient = new BlobContainerClient(this.azureStorageConnectionString, containerName);// creoamos el blob conteiner client, y mandamos la cadena de conexion
            var blobClient = blobContainerClient.GetBlobClient(blobFilename);//Accedo al archivo 

            try
            {
                await blobClient.DeleteAsync();
            }
            catch
            {
            }
        }

    

        public async Task<string> UploadAsync(IFormFile file, ContainerEnum container, string blobName = null)
        {
            if (file.Length == 0) return null;//para ver si existe el archivo

            var containerName = Enum.GetName(typeof(ContainerEnum), container).ToLower();// si tiene peso obtengo el nombre del contenedor por medio del enum

            var blobContainerClient = new BlobContainerClient(this.azureStorageConnectionString, containerName);//aca indicamos la cadena de conexion y a que contenedor me quiero conectar 

            // Get a reference to the blob just uploaded from the API in a container from configuration settings
            if (string.IsNullOrEmpty(blobName))//validadndo que si este pasando un nombre en el blobName
            {
                blobName = Guid.NewGuid().ToString();//por medio de la clase guid obtenemos una instancia que nos da un valor alfa numerico
            }

            var blobClient = blobContainerClient.GetBlobClient(blobName);// acedo al atributo 

            var blobHttpHeader = new BlobHttpHeaders { ContentType = file.ContentType };//aca sabremos cual es el contentType, si es jpg, png....

            // Open a stream for the file we want to upload
            await using (Stream stream = file.OpenReadStream())//obtenemos el string
            {
                // Upload the file async
                await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
            }

            return blobName;//retornamos el nombre del archivo 
        }
    }


    public interface IAzureBlobStorageService
    {
        /// <summary>
        /// This method uploads a file submitted with the request
        /// </summary>
        /// <param name="file">File for upload</param>
        /// <param name="container">Container where to submit the file</param>
        /// <param name="blobName">Blob name to update</param>
        /// <returns>File name saved in Blob contaienr</returns>
        Task<string> UploadAsync(IFormFile file, ContainerEnum container, string blobName = null);

        /// <summary>
        /// This method deleted a file with the specified filename
        /// </summary>
        /// <param name="container">Container where to delete the file</param>
        /// <param name="blobFilename">Filename</param>
        Task DeleteAsync(ContainerEnum container, string blobFilename);
    }
}
