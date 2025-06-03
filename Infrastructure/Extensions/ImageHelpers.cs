using Application.DTOs;
using Application.DTOs.Response;
using Application.Extentions;

namespace Infrastructure.Extensions
{
    public static class ImageHelpers
    {
        public static string LoadImage(string ProductImageName)
        {
            try
            {
                var fileName = @"C:\Images\Products\" + ProductImageName;
                if (File.Exists(fileName))
                {
                    var imageArray = File.ReadAllBytes(fileName);
                    var base64Image = Convert.ToBase64String(imageArray);

                    //dồn chung ImageName và string base64 của ảnh trả về cho client cắt ra xử
                    var typeImage = ProductImageName.Split('.')[1];
                    if (typeImage == "png")
                    {
                        ProductImageName = $"data:image/png;base64,{base64Image}";
                    }
                    else if (typeImage == "jpeg" || typeImage == "jpg")
                    {
                        ProductImageName = $"data:image/jpeg;base64,{base64Image}";
                    }
                    else if (typeImage == "svg")
                    {
                        ProductImageName = $"data:image/svg+xml;base64,{base64Image}";
                    }
                }
                else ProductImageName = string.Empty;


                return ProductImageName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
