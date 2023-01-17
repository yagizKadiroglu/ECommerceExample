using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.File
{
    public class FileService : IFileService
    {
        readonly IHostingEnvironment _webHostEnvironment;

        public FileService(IHostingEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }

        }



        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            
            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (var file in files)
            {
                Guid guid = Guid.NewGuid();
                string fullPath = Path.Combine(uploadPath, $"{guid}{Path.GetExtension(file.FileName)}");

                bool result = await CopyFileAsync(fullPath, file);
                datas.Add(($"{guid}{Path.GetExtension(file.FileName)}",fullPath));
                results.Add(result);
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;
            return null;
        }
    }
}

