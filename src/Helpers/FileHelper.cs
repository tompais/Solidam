using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helpers
{
    public class FileHelper
    {
        public static string GuardarArchivo(HttpPostedFileBase archivoTransferencia)
        {
            var path = HttpContext.Current.Server.MapPath("~/Content/UploadedFiles/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = Path.GetExtension(archivoTransferencia.FileName);
            var newFileName = $"{RandomTokenGenerator.GetTokenBySize(16)}{extension}";
            archivoTransferencia.SaveAs($"{path}{newFileName}");
            return newFileName;
        }
    }
}
