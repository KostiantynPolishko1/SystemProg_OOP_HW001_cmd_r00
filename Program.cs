using System.Diagnostics;

namespace SystemProg_OOP_HW001_cmd_r00
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("\tname of App: ");
                string? appName = Console.ReadLine();

                if (appName == null || appName == string.Empty)
                {
                    throw new Exception($"No enter name off App");
                }
                else
                {
                    try
                    {
                        StatusApp(IsLaunchApp(Process.GetProcesses().ToList(), appName));
                    }
                    catch (InvalidOperationException ex)
                    {
                        ShowMsgExp($"\tError!\n\t{appName} does not have an identifier.");
                    }
                    catch (NotSupportedException ex)
                    {
                        ShowMsgExp($"\tError!\n\t{appName} is not on this computer.");
                    }
                    catch (Exception ex)
                    {
                        ShowMsgExp(ex.Message);
                    }
                }
            }
            catch (Exception MyEx)
            {
                ShowMsgExp(MyEx.Message);
            }

            void ShowMsgExp(in string msg)
            {
                Console.WriteLine($"\tError!\n\t{msg}");
            }

            void StatusApp((bool flag, string appName) process)
            {
                if (process.flag) 
                { 
                    Console.WriteLine($"{process.appName} is lunched"); 
                }
                else 
                { 
                    Console.WriteLine($"{process.appName} is not lunched"); 
                }
            }

            (bool, string) IsLaunchApp(in List<Process> processes, string appName)
            {
                appName = appName.ToLower();

                foreach (Process process in processes)
                {
                    if (process.ProcessName.ToLower().IndexOf(appName) != -1) 
                    { 
                        return (true, process.ProcessName); 
                    }
                }

                return (false, appName);
            }
        }
    }
}