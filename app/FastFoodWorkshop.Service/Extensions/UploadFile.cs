namespace FastFoodWorkshop.Service.Extensions
{
    using Common.WebConstants;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading.Tasks;

    public static class UploadFile
    {
        public static async Task<byte[]> UploadAsync(this IFormFile file)
        {
            byte[] result = null;

            if (file != null && file.Length <= NumericConstants.PictureSizeLimit)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    result = memoryStream.ToArray();
                }
            }

            return result;
        }
    }
}
