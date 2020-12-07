using System;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
           
        }
        public void Do(string path)
        {
            string message = "";
            string @finally = "";
            var files = GetFiles(path);
            var images = GetImages(files,out message);
            CreateGif(images,out @finally);
            MessageBox.Show(message+Environment.NewLine+@finally);

        }
        private static string[] GetFiles(string path)
        {
            var files = Directory.GetFiles(path);
            ShowFiles(files);
            return files;
        }
        protected static string ShowFiles(string[] files)
        {
            string str = "";
            foreach (string fileName in files)
            {
                var name = fileName.Split('/');
                str += name[name.Length - 1] + Environment.NewLine;
            }
           return str;
        }
        private static string[] GetImages(string[] files, out string st)
        {
            st = "";
            var imageRegex = new Regex(@"(\.jpg|\.png|\.gif|\.tiff?)", RegexOptions.IgnoreCase);
            st += "Get images..." + Environment.NewLine;
            string str = "";
            foreach (string fileName in files)
            {
                if (imageRegex.IsMatch(fileName))
                {
                    if (str != "")
                    {
                        str += " " + fileName;
                    }
                    else
                    {
                        str += fileName;
                    }
                    var name = fileName.Split('/');
                  
                }
                st += str;
            }
            if (str == "")
            {
                st+="Nothing was found"+Environment.NewLine;
            }
            var images = str.Split(' ');
            return images;
        }
        private static void CreateGif(string[] imagesPath,out string str)
        {
            str = "";
            var imageRegex = new Regex(@"(\.jpg|\.png|\.gif|\.tiff?)", RegexOptions.IgnoreCase);

            if (imagesPath != null)
            {
                str += "Gifs created"+Environment.NewLine;
            }
            foreach (string image in imagesPath)
            {
                if (!image.Contains(".gif"))
                {
                    string name = imageRegex.Replace(image, ".gif");
                    str += ("gif path: " + name + Environment.NewLine);
                    Image img = Image.FromFile(image, true);
                    // Bitmap bmp = (Bitmap)Image.FromFile(@"C:\Users\Xiaomi\source\repos\lab4\pigeon\p1.jpg", true);
                    img.Save(name, System.Drawing.Imaging.ImageFormat.Gif);
                }
            }
        }
    }
}
