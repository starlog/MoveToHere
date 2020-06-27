using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MoveToRoot
{
    class Program
    {
        const string VERSION = "0.5";
        private static bool isArgs = false;
        private static bool isProcess = false;

        static void Main(string[] args)
        {
            string param = "";

            if (args.Length == 1)
            {
                if (args[0].ToLower() == "/ver")
                {
                    System.Console.WriteLine("Version " + VERSION);
                    return;
                }
                else if (args[0].ToLower() == "/p")
                {
                    isProcess = true;
                }
                else
                {
                    param = args[0];
                    isArgs = true;
                }
            }

            if (!isArgs)
            {
                param = @".";
            }

            System.Console.WriteLine("MoveToRoot Tool by Felix");

            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(param);
                DirectoryInfo[] subdirs = dInfo.GetDirectories();

                foreach (DirectoryInfo MyD in subdirs)
                {
                    System.Console.WriteLine(@"Processing..." + MyD.FullName);
                    foreach (FileInfo MyF in MyD.GetFiles())
                    {
                        System.Console.WriteLine(MyF.Name);

                        if (File.Exists(@".\" + MyF.Name))
                        {
                            File.Delete(@".\" + MyF.Name);
                        }
                        File.Move(MyF.FullName, @".\" + MyF.Name);

                        if ((MyF.Extension == ".txt" || MyF.Extension == ".nfo") && isProcess)
                        {
                            File.Delete(@".\" + MyF.Name);
                        }
                    }
                    Directory.Delete(MyD.FullName, true);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
