using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Classes
{
    class clsUtil
    {
        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
               
            }
            return true;
        }

        public static string ReplaceFileNameWithGUID(string SourceFile)
        {
            if (string.IsNullOrEmpty(SourceFile))
                return "";

            FileInfo Fi = new FileInfo(SourceFile);

            string extn = Fi.Extension;
            return Guid.NewGuid().ToString() + extn;
        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            if (string.IsNullOrEmpty(sourceFile))
                return false;


            string DestinationFolder = @"C:\DVLD-People-Images\";

            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }
            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                // نسخ الملف من مكانه الأصلي إلى مجلد الصور الجديد
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // تحديث مسار الملف ليدل على المسار الجديد الدائم
            sourceFile = destinationFile;
            return true;
        
    }
    }
}
