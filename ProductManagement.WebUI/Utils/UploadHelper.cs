using Microsoft.Extensions.Hosting;

namespace ProductManagement.WebUI.Helpers {
    public static class UploadHelper {
        public static async Task<string> UploadImageAsync(IWebHostEnvironment hostEnvironment, IFormFile img) {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = FileNameControl(Path
                .GetFileNameWithoutExtension(img.FileName));
            string extension = Path.GetExtension(img.FileName);
            fileName = fileName + DateTime.Now.ToString("yyyyMMddss") + extension;

            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create)) {
                await img.CopyToAsync(fileStream);
            }
            return "/Image/" + fileName;
        }
        private static string FileNameControl(string filename) {
            filename.Replace("ş", "s");
            filename.Replace("ç", "c");
            filename.Replace("ü", "u");
            filename.Replace("ğ", "g");
            filename.Replace("ı", "i");
            filename.Replace("ö", "o");

            filename.Replace("Ş", "S");
            filename.Replace("Ç", "C");
            filename.Replace("Ü", "U");
            filename.Replace("Ğ", "G");
            filename.Replace("İ", "I");
            filename.Replace("Ö", "O");


            filename.Replace("-", "");
            filename.Replace("_", "");
            filename.Replace("?", "");
            filename.Replace("!", "");
            filename.Replace("#", "");
            filename.Replace("*", "");
            filename.Replace("/", "");
            filename.Replace("&", "");
            filename.Replace("%", "");
            filename.Replace("$", "");
            filename.Replace("^", "");
            filename.Replace("{", "");
            filename.Replace("}", "");
            filename.Replace("[", "");
            filename.Replace("]", "");
            filename.Replace(")", "");
            filename.Replace("(", "");
            filename.Replace(":", "");





            return filename;

        }
    }
}