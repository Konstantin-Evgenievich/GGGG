using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Diplom.Controllers
{

    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment appEnvironment;
        public AdminController(IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }
        [HttpPost] 
        public void SaveFile(IFormFile previewImage)
        {

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + "/HH/test.pdf", FileMode.Create))
            {
                previewImage.CopyTo(fileStream);

            }
        }
       
      }
  }

