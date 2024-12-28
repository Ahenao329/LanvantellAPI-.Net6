




using LavantellAPIS.Context;
using LavantellAPIS.Models;
using LavantellAPIS.Models.Request;
using LavantellAPIS.Services;

namespace LavantellAPIS.Repositories
{
    public class LavantellRepository : InosotrosRepository
    {
        private readonly IAzureBlobStorageService azureBlobStorageService;
        private readonly ApplicationDbContext context;

        public LavantellRepository(IAzureBlobStorageService azureBlobStorageService, ApplicationDbContext context)
        {
            this.azureBlobStorageService = azureBlobStorageService;
            this.context = context;
        }
        public async Task<Nosotros> Add(NosotrosRequest request)
        {
            var Nosotros = new Nosotros()
            {
                Descripcion = request.Descripcion,
            };

            if (request.Image != null)
            {
                Nosotros.Imagen = await this.azureBlobStorageService.UploadAsync(request.Image, Enums.ContainerEnum.IMAGES);
            }

            //if (request.TechnicalDataSheet != null)
            //{
            //    car.TechnicalDataSheetPath = await this.azureBlobStorageService.UploadAsync(request.TechnicalDataSheet, Enums.ContainerEnum.DOCUMENTS);
            //}

            this.context.Nosotros.Add(Nosotros);
            this.context.SaveChanges();

            return Nosotros;
        }

        public async Task Delete(int Id)
        {

            var    nosotros = this.context.Nosotros.Find(Id);//OBTENGO EL VH
            if (nosotros != null)//VALIDO SI ES DIFERENTE DE NULO
            {

                if (!string.IsNullOrEmpty(nosotros.Imagen))//VALIDO SI TIENE DOCUMENTOS
                {
                    await this.azureBlobStorageService.DeleteAsync(Enums.ContainerEnum.IMAGES, nosotros.Imagen);//SI TTIENE LOS ELIMINO

                }


                //if (!string.IsNullOrEmpty(car.TechnicalDataSheetPath))
                //{
                //    await this.azureBlobStorageService.DeleteAsync(Enums.ContainerEnum.DOCUMENTS, car.TechnicalDataSheetPath);
                //}
                this.context.Nosotros.Remove(nosotros);
                this.context.SaveChanges();
            }
        }
        public List<Nosotros> getAll()
        {
            return this.context.Nosotros.ToList();
        }

        public Nosotros GetById(int Id)
        {
            return this.context.Nosotros.Find(Id);
        }

        public async Task<Nosotros> Update(int id, NosotrosRequest request)
        {
            var nosotros = this.context.Nosotros.Find(id);
            if (nosotros != null)
            {
                nosotros.Descripcion = request.Descripcion;


                if (request.Image != null)
                {
                    nosotros.Imagen = await this.azureBlobStorageService.UploadAsync(request.Image, Enums.ContainerEnum.IMAGES, nosotros.Imagen);
                }

                //if (request.TechnicalDataSheet != null)
                //{
                //    nosotros.TechnicalDataSheetPath = await this.azureBlobStorageService.UploadAsync(request.TechnicalDataSheet, Enums.ContainerEnum.DOCUMENTS, nosotros.TechnicalDataSheetPath);
                //}

                this.context.Update(nosotros);
                this.context.SaveChanges();
            }

            return nosotros;
        }
    }



    }

    public interface InosotrosRepository
    {
        List<Nosotros> getAll();
        Nosotros GetById(int Id);
        Task<Nosotros> Add(NosotrosRequest request);
        Task<Nosotros> Update(int Id, NosotrosRequest request);
        Task Delete(int Id);
    }


