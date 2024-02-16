using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace TestRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Внесение изменений в реестр");

            if (args.Length == 0)
            {
                //Console.WriteLine("First");
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkyWay\BIM Manager\UST_ProjectManagement.exe";
                string exepath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                //Console.WriteLine(exepath);
                //MessageBox.Show(exepath);
                ProcessStartInfo processInfo = new ProcessStartInfo(); //создаем новый процесс
                processInfo.Verb = "runas"; //в данном случае указываем, что процесс должен быть запущен с правами администратора  
                processInfo.Arguments = path;
                //processInfo.FileName = @"Z:\BIM01\07_Soft\RegistryManager\RegistryManager.exe"; //указываем исполняемый файл (программу) для запуска
                processInfo.FileName = exepath; //указываем исполняемый файл (программу) для запуска
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                try
                {
                    Process.Start(processInfo); //пытаемся запустить процесс
                }
                catch
                {
                    //Ничего не делаем, потому что пользователь, возможно, нажал кнопку "Нет" в ответ на вопрос о запуске программы в окне предупреждения UAC (для Windows 7)
                }
                //Application.Exit();
                //Console.WriteLine("Выход из первой программы");
            }
            else
            {
                try
                {
                    string path = "";
                    for(int i = 0; i < args.Length; i++)
                    {
                        if (i > 0) path += " ";
                        path += args[i];
                        
                    }
                    //Console.WriteLine(path);
                    //Console.WriteLine(args.Length);
                    try
                    {
                        RegistryKey classesRoot = Registry.ClassesRoot;
                        RegistryKey licenseKey = classesRoot.CreateSubKey("ustpm");
                        licenseKey.SetValue(default, "URL:Ustpm Protocol");
                        licenseKey.SetValue("URL Protocol", "");

                        RegistryKey defaultIconKey = licenseKey.CreateSubKey("DefaultIcon");
                        defaultIconKey.SetValue(default, "UST_ProjectManagement.exe,1");

                        RegistryKey shellKey = licenseKey.CreateSubKey("shell");

                        RegistryKey openKey = shellKey.CreateSubKey("open");

                        RegistryKey commandKey = openKey.CreateSubKey("command");
                        commandKey.SetValue(default, $"{path} {"%1"}");
                    }
                    catch
                    {
                    }

                    //Console.WriteLine("Готов. Нажмите любую клавишу.");
 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                }
                //Console.ReadKey();
            }


            //string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkyWay\BIM Manager\UST_ProjectManagement.exe";
            //Console.WriteLine(path);

            //RegistryKey classesRoot = Registry.ClassesRoot;
            //RegistryKey licenseKey = classesRoot.CreateSubKey("ustpm");
            //licenseKey.SetValue(default, "URL:Ustpm Protocol");
            //licenseKey.SetValue("URL Protocol", "");

            //RegistryKey defaultIconKey = licenseKey.CreateSubKey("DefaultIcon");
            //defaultIconKey.SetValue(default, "UST_ProjectManagement.exe,1");

            //RegistryKey shellKey = licenseKey.CreateSubKey("shell");

            //RegistryKey openKey = shellKey.CreateSubKey("open");

            //RegistryKey commandKey = openKey.CreateSubKey("command");
            //commandKey.SetValue(default, $"{path} {"%1"}");

            //Console.WriteLine("Готов. Нажмите любую клавишу.");
            //Console.ReadKey();
        }
    }
}
