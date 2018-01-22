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

        public int UploadTheImage(HttpPostedFileBase image, String identity)
        {

            if (db.Guardians.Any(m => m.GuardianID.ToString() == identity))
            {
                Guardian guardianToUpdate = db.Guardians.First(m => m.GuardianID.ToString() == identity);
                guardianToUpdate.Photo = ConvertToBytes(image);
            }
            if (db.Students.Any(m => m.StudentID.ToString() == identity))
            {
                Student studentToUpdate = db.Students.First(m => m.StudentID.ToString() == identity);
                studentToUpdate.Photo = ConvertToBytes(image);
            }
            if (db.Teachers.Any(m => m.TeacherID.ToString() == identity))
            {
                Teacher teacherToUpdate = db.Teachers.First(m => m.TeacherID.ToString() == identity);
                teacherToUpdate.Photo = ConvertToBytes(image);
            }
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