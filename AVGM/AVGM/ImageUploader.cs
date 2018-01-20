using AVGM.DAL;
using AVGM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AVGM
{
    public class ImageUploader
    {
        private SchoolContext db = new SchoolContext();

        public ImageUploader() { }

        public int UploadTheGuardianImage(HttpPostedFileBase image, String identity)
        {
            Guardian guardianToUpdate = db.Guardians.First(m => m.Email == identity);
            guardianToUpdate.Photo = ConvertToBytes(image);
            int i = db.SaveChanges();
            return (i == 1 ? 1 : 0);
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }

}