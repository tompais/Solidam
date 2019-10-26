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
        public static void GuardarArchivo(HttpPostedFileBase archivoTransferencia)
        {
            if (archivoTransferencia != null)
            {
                string path = HttpContext.Current.Server.MapPath("~/Content/UploadedFiles/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                archivoTransferencia.SaveAs(path + Path.GetFileName(archivoTransferencia.FileName));
            }
        }
    }
}
