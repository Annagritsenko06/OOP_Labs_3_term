using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Diagnostics;
namespace Lab12
{
    class GAALog
    {
        private string FilePath = "GAAlogfile.txt";
        public void WriteToFile(string action,string details)
        {
            string information = $"{DateTime.Now}: Action - {action}, details - {details}";
            using (StreamWriter sw = new StreamWriter(FilePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(information);
            }
        }
        public List<string> ReadFromFile()
        {
            if(!File.Exists(FilePath))
            {
                return new List<string>();
            }
            return File.ReadAllLines(FilePath).ToList();
        }
        public List<string> SearchInfo(string info)
        {
            return File.ReadAllLines(FilePath).Where(i => i.Contains(info)).ToList();
        }
        public void DisplaySearchResults(string info)
        {
            List<string> results = SearchInfo(info);

            if (results.Count == 0)
            {
                Console.WriteLine("Ничего не найдено.");
            }
            else
            {
                Console.WriteLine("Найденные записи:");
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
        }
        public List<string> SearchByDate(DateTime startDate, DateTime endDate)
        {
            return File.ReadAllLines(FilePath)
                .Where(line => DateTime.TryParse(line.Substring(0, line.IndexOf(':')), out DateTime logDate) &&
                               logDate >= startDate && logDate <= endDate).ToList();
        }

        public int CountRecords()
        {
            return File.ReadAllLines(FilePath).Length;
        }

        public void RemoveOldRecords()
        {
            var currentHour = DateTime.Now.Hour;
            var records = File.ReadAllLines(FilePath)
                .Where(line => DateTime.TryParse(line.Substring(0, line.IndexOf(':')), out DateTime logDate) &&
                               logDate.Hour == currentHour)
                .ToList();

            File.WriteAllLines(FilePath, records);
        }
    }
    class GAADiskInfo
    {
        public void FreeSpace()
        {
           
            DriveInfo [] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Свободное месте на диске - {drive.AvailableFreeSpace}");
            }
        }
        public void FileSystem()
        {

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine(drive.Name);
                Console.WriteLine($"Имя файловой системы - {drive.DriveFormat}");
            }
        }
        public void InfoAboutDrivers() {
            DriveInfo [] drives = DriveInfo.GetDrives();
        foreach(DriveInfo drive in drives){
                
                if (drive.IsReady)
                {
                    Console.WriteLine(drive.Name);
                    Console.WriteLine($"Объем диска - {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство - {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка диска - {drive.VolumeLabel}");
                }
        }
            }

    }
    class GAAFileInfo
    {
        private string FilePath = "GAAlogfile.txt";
        public void FileInfo()
        {
            
        FileInfo file = new FileInfo(FilePath);
            if (file.Exists)
            {
                Console.WriteLine($"Полное имя файла: {file.FullName}");
                Console.WriteLine($"Размер файла: {file.Length}");
                Console.WriteLine($"Расширение файла: {file.Extension}");
                Console.WriteLine($"Имя файла: {file.Name}");
                Console.WriteLine($"Дата создания файла: {file.CreationTime}");
                Console.WriteLine($"Информация об изменениях: {file.LastWriteTime}");

            }
        }
    }
    class GAADirInfo
    {
        public void DirInfo(string nameOdDir)
        {
            if (Directory.Exists(nameOdDir))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(nameOdDir);
                Console.WriteLine($"Имя директория: {dirInfo.FullName} ");
                Console.WriteLine($"Количестве файлов: {dirInfo.GetFiles().Length} ");
                Console.WriteLine($"Время создания: {dirInfo.CreationTime} ");
                Console.WriteLine($"Количестве поддиректориев: {dirInfo.GetDirectories().Length}");
                Console.WriteLine($"Список родительских директориев: {dirInfo.Parent?.FullName}");
            }
        }
    }
    class GAAFileManager
    {
        public void Method_1(string directory)
        {

            string inspectDir = Path.Combine(directory, "GAAInspect");
            Directory.CreateDirectory(inspectDir);
            string dirInfoFile = Path.Combine(inspectDir, "GAAdirinfo.txt");

            using (StreamWriter sw = new StreamWriter(dirInfoFile))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    sw.WriteLine(file);
                }
            }

            string fileToCopy = Directory.GetFiles(directory).FirstOrDefault();
            if (fileToCopy != null)
            {
                string copiedFile = Path.Combine(inspectDir, "CopiedFile" + Path.GetExtension(fileToCopy));
                File.Copy(fileToCopy, copiedFile);
                File.Delete(fileToCopy);
            }
        }

        public void Method_2(string newDir, string directory, string extention)
        {

            Directory.CreateDirectory(newDir);
            foreach (var file in Directory.GetFiles(directory, $"*{extention}"))
            {
                var path = Path.Combine(newDir, Path.GetFileName(file));
                File.Copy(file, path);

            }

        }
        public void Method_3(string directory, string dir2)
        {
            try
            {
                
                if (!Directory.Exists(directory))
                {
                    Console.WriteLine("Каталог-источник не существует.");
                    return;
                }

                if (Directory.Exists(dir2))
                {
                    Console.WriteLine("Каталог назначения уже существует.");
                    return;
                }

                Directory.Move(directory, dir2);
                Console.WriteLine($"Каталог успешно перемещён из {directory} в {dir2}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при перемещении каталога: {ex.Message}");
            }
        }

        public void Method_4(string directory, string nameZip)
        {
            ZipFile.CreateFromDirectory(directory, nameZip);
        }
        public void Method_5(string directory, string nameZip)
        {
            ZipFile.ExtractToDirectory(nameZip, directory);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            GAALog gAALog = new GAALog();
            gAALog.WriteToFile("Создан файл", " с именем GAAlogfile.txt ");
            gAALog.ReadFromFile();
            gAALog.DisplaySearchResults("Stream");
            GAAFileInfo info = new GAAFileInfo();
            info.FileInfo();
            GAADiskInfo diskInfo = new GAADiskInfo();
            diskInfo.FreeSpace();
            diskInfo.FileSystem();
            diskInfo.InfoAboutDrivers();

            GAADirInfo dirInfo = new GAADirInfo();
            dirInfo.DirInfo("C:\\Users\\admin\\Documents\\уник\\3 СЕМ");

            GAAFileManager fileManager = new GAAFileManager();
            fileManager.Method_1("C:\\Users\\admin\\Documents\\уник\\3 СЕМ\\ООП\\папка_для_лабы");
            fileManager.Method_2("C:\\Users\\admin\\Documents\\уник\\3 СЕМ\\ООП\\папка_для_лабы\\newDir", "C:\\Users\\admin\\Documents\\уник\\3 СЕМ\\ООП\\папка_для_лабы\\GAAInspect", "txt");
            fileManager.Method_3("C:\\Users\\admin\\Documents\\уник\\3 СЕМ\\ООП\\папка_для_лабы\\GAAInspect", "C:\\Users\\admin\\Documents\\уник\\3 СЕМ\\ООП\\папка_для_лабы\\newDir");
            fileManager.Method_4("C:\\Users\\admin\\Documents\\уник\\3 СЕМ\\ООП\\папка_для_лабы", "Dir.zip");
            fileManager.Method_5("C:\\Users\\admin\\Documents\\уник\\3 СЕМ\\ООП\\12 лаба\\Lab_12\\Lab_12\\Новая папка", "Dir.zip");
            DateTime startDate = new DateTime(2024, 12, 17, 0, 0, 0);
            DateTime endDate = new DateTime(2024, 12, 17, 23, 59, 59);
            var records = gAALog.SearchByDate(startDate, endDate);
            Console.WriteLine($"Записи за {startDate.ToShortDateString()}: {records.Count}");

            int recordCount = gAALog.CountRecords();
            Console.WriteLine($"Общее количество записей: {recordCount}");

            gAALog.RemoveOldRecords();
            Console.WriteLine("Старые записи удалены, оставлены только записи за текущий час.");                                                                                                           //6 задание с файлами не сделано
        }
    }
}