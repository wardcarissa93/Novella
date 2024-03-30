using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System;
using Novella.ViewModels;
using Novella.EfModels;
using Microsoft.AspNetCore.Mvc;

namespace Novella.Repositories
{
    public class ImageRepo
    {
        private readonly NovellaContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageRepo(NovellaContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public FileResult GetImage(int productId)
        {
            var image = _db.ImageStores.FirstOrDefault(i => i.FkProductId == productId);
            if (image != null)
            {
                return new FileContentResult(image.Image, "image/jpeg");
            }
            else
            {
                // Return a default image or handle the case where the image is not found
                var defaultImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "404_img.jpg");
                return new FileStreamResult(File.OpenRead(defaultImagePath), "image/jpeg");
            }
        }

        public string GetImageUrl(int productId)
        {
            var image = _db.ImageStores.FirstOrDefault(i => i.FkProductId == productId);
            if (image != null)
            {
                // Assuming you have a property in ImageStore table to store the image URL
                return "/Home/GetImage?productId=" + productId;
            }
            else
            {
                // Return the URL for the default image
                return "/Images/404_img.jpg";
            }
        }
    }
}
