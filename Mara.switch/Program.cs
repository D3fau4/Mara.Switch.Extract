using LibHac.Fs;
using System;
using System.IO;

namespace Mara.Switch
{
    class Program
    {
        public static HOS horizon;
        public static PartitionFS NSP;
        public static GameCard XCI;
        public static NCA NCAS;
        static void Main(string[] args)
        {
            horizon = new HOS(args[0]);
            if (args[1].Contains(".xci"))
            {
                XCI = new GameCard(horizon, args[1]);
                try
                {
                    var update = new PartitionFS(args[2]);
                    NCAS = new NCA(horizon, XCI.MountGameCard(horizon), update.MountPFS0(horizon, "update"));
                }
                catch (Exception)
                {
                    NCAS = new NCA(horizon, XCI.MountGameCard(horizon));
                }
            } 
            else
            {
                NSP = new PartitionFS(args[1]);
                try
                {
                    var update = new PartitionFS(args[2]);
                    NCAS = new NCA(horizon, NSP.MountPFS0(horizon, "base"), update.MountPFS0(horizon, "update"));
                }
                catch (Exception)
                {
                    NCAS = new NCA(horizon, NSP.MountPFS0(horizon, "base"));
                }
            }
            NCAS.MountProgram(horizon, args[args.Length - 1]);
            bool end = false;
            do
            {
                Console.WriteLine("Escoge una opción: \n1) Extraer Romfs y Exefs\n2) Extraer solo romfs\n3) Extraer solo exefs\n4) Salir");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        FSUtils.MountFolder(horizon.horizon.Fs, "exefs", "OutExefs");
                        foreach (DirectoryEntryEx entry in horizon.horizon.Fs.EnumerateEntries("exefs:/"))
                        {
                            Console.WriteLine(entry.FullPath);
                            var res = FSUtils.CopyFile(horizon.horizon.Fs, entry.FullPath, entry.FullPath.Replace("exefs:/", "OutExefs:/"));
                            Console.WriteLine(res);
                        }
                        FSUtils.MountFolder(horizon.horizon.Fs, "romfs", "OutRomfs");
                        foreach (DirectoryEntryEx entry in horizon.horizon.Fs.EnumerateEntries("romfs:/"))
                        {
                            Console.WriteLine(entry.FullPath);
                            var res  = FSUtils.CopyFile(horizon.horizon.Fs, entry.FullPath, entry.FullPath.Replace("romfs:/", "OutRomfs:/"));
                            Console.WriteLine(res);
                        }
                        break;
                    case "2":
                        FSUtils.MountFolder(horizon.horizon.Fs, "romfs", "OutRomfs");
                        foreach (DirectoryEntryEx entry in horizon.horizon.Fs.EnumerateEntries("romfs:/"))
                        {
                            Console.WriteLine(entry.FullPath);
                            var res = FSUtils.CopyFile(horizon.horizon.Fs, entry.FullPath, entry.FullPath.Replace("romfs:/", "OutRomfs:/"));
                            Console.WriteLine(res);
                        }
                        break;
                    case "3":
                        FSUtils.MountFolder(horizon.horizon.Fs, "exefs", "OutExefs");
                        foreach (DirectoryEntryEx entry in horizon.horizon.Fs.EnumerateEntries("exefs:/"))
                        {
                            Console.WriteLine(entry.FullPath);
                            var res = FSUtils.CopyFile(horizon.horizon.Fs, entry.FullPath, entry.FullPath.Replace("exefs:/", "OutExefs:/"));
                            Console.WriteLine(res);
                        }
                        break;
                    case "4":
                        end = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
            while (end != true);
        }
    }
}
