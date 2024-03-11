using System.Diagnostics;

namespace SystemProg_OOP_HW001_cmd_r00
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*            try
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
                        }*/



            //ShowListApp(Process.GetProcesses().ToList());

            HashSet<(string, int)> tmp = GetNamesPriorities(Process.GetProcesses().ToList());
            ShowListApp(tmp, 7);

            HashSet<(string, int)> GetNamesPriorities(in List<Process> processs)
            {
                HashSet<(string, int)> NamesPriorities = new HashSet<(string, int)>() { };
                foreach (Process p in processs)
                {
                    NamesPriorities.Add((p.ProcessName, p.BasePriority));
                }

                return NamesPriorities;
            }

            void ShowListApp(in HashSet<(string, int)> process, int index = 0)
            {
                int i = 0;
                foreach ((string name, int priority) item in process)
                {
                    if(item.priority > index)
                    {
                        Console.WriteLine($"{i += 1}. {item.name} - {item.priority}");
                    }
                }
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