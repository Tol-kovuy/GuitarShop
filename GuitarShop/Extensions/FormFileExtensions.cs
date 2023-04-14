using GuitarShop.BLL.Models;

namespace GuitarShop.Extensions
{
    public static class FormFileExtensions
    {
        public static byte[] GetBytes(this IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            var byteArray = memoryStream.ToArray();
            return byteArray;
        }
        public static IFormFile ConvertToIFormFile(byte[] image)
        {
            if (image == null)
            {
                return null;
            }
            var contentType = "image/jpeg";
            using var stream = new MemoryStream(image);
            IFormFile file = new FormFile(stream, 0, stream.Length, null, "FUCK")
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
            return file;
        }
       
    }
}
