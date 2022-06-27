using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.Utilities
{
    public static class Extension
    {
        #region Check Image

        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }

        public static bool CheckFileSize(this IFormFile file, int size)
        {
            return file.Length / 1024 <= size;
        }

        public async static Task<string> SaveFileAsync(this IFormFile file, string root, params string[] folder)
        {
            string filename = Guid.NewGuid().ToString() + "_" + file.FileName;
            string resultPath = Path.Combine(root, folder[0], folder[1], filename);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filename;
        }

        #endregion
    }
}
