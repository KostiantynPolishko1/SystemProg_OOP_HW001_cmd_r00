using System.Diagnostics;

namespace SystemProg_OOP_HW001_cmd_r00
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //=====Varification if App is presented among process launched=====//

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

            //=====Show App with BasePriority > 7=====//

/*            HashSet<(string, int)> tmp = GetNamesPriorities(Process.GetProcesses().ToList());
            ShowListAppNamePriority(tmp, 7);
*/

            //=====Close All chrome App=====//

/*          SortedSet<string> _listApp = GetNamesApp(Process.GetProcesses().ToList());
            ShowListAppName(_listApp);

            List<Process> listAppByName = Process.GetProcessesByName("chrome").ToList();

            foreach(Process process in listAppByName)
            {
                Console.WriteLine($"{process.ProcessName} - {process.Id}");

                (bool flag, string Msg) = CloseApp(Process.GetProcessById(process.Id));
                if (!flag)
                {
                    Console.WriteLine(Msg);
                }
            }*/

            //=====Close App by ID inputed=====//

            /*SortedSet<string> _listApp = GetNamesApp(Process.GetProcesses().ToList());
            ShowListAppName(_listApp);

            Console.Write("\tApp name to close -> ");
            string nameApp = Console.ReadLine();
            ShowListAppNameId(Process.GetProcessesByName(nameApp).ToList());

            Console.Write("\tApp Id to close -> ");
            bool flag = int.TryParse(Console.ReadLine(), out int AppId);

            if (flag)
            {
                try
                {
                    (bool flag2, string Msg) = CloseApp(Process.GetProcessById(AppId));

                    if(!flag2)
                    {
                        throw new Exception(Msg);
                    }

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("The identifier might be expired.");
                }
                catch (Exception MyEx)
                {
                    Console.WriteLine(MyEx);
                }
            }*/

            (bool, string) CloseApp(in Process App)
            {
                try
                {
                    for(int i = 0; i < 5; i++)
                    {
                        if (!App.HasExited)
                        {
                            App.Refresh();
                            Thread.Sleep(2000);
                        }
                        else { break; }
                    }
                    
                    App.CloseMainWindow();
                    App.Close();

                    return (true, string.Empty);
                }
                catch(InvalidOperationException ex)
                { 
                    return (false, $"{App.ProcessName} is already terminated");
                }
                catch(Exception ex)
                {
                    return (false, $"{App.ProcessName} is failed");
                }
            }

            SortedSet<string> GetNamesApp(in List<Process> processes)
            {
                SortedSet<string> listApp = new SortedSet<string>();
                foreach (Process p in processes)
                {
                    listApp.Add(p.ProcessName);
                }

                return listApp;
            }

            bool IsNameApp(in string nameApp, SortedSet<string> _listApp)
            {
                foreach (string app in _listApp)
                {
                    if (app == nameApp) { return true; }
                }

                return false;
            }

            void ShowListAppNamePriority(in HashSet<(string, int)> process, int index = 0)
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

            void ShowListAppName(in SortedSet<string> _listApp)
            {
                int i = 0;
                foreach (string name in _listApp)
                {
                    Console.WriteLine($"{i += 1}. {name}");
                }
            }

            void ShowListAppNameId(in List<Process> listApp)
            {
                foreach(Process p in listApp)
                {
                    if (!p.HasExited)
                    {
                        Console.WriteLine($"{p.ProcessName} - {p.Id}");
                    }
                }
            }


            void ShowMsgExp(in string msg)
            {
                Console.WriteLine($"\tError!\n\t{msg}");
            }

            HashSet<(string, int)> GetNamesPriorities(in List<Process> processs)
            {
                HashSet<(string, int)> NamesPriorities = new HashSet<(string, int)>() { };
                foreach (Process p in processs)
                {
                    NamesPriorities.Add((p.ProcessName, p.BasePriority));
                }

                return NamesPriorities;
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