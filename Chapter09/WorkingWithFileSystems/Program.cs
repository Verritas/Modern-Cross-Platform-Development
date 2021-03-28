using System;
using System.IO;
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Xml;
using System.IO.Compression;

namespace WorkingWithFileSystems
{
    class Program
    {
        static void Main(string[] args)
        {
            //OutputFileSystemInfo();
            //WorkWithDrives();
            //WorkWithDirectories();
            //workWithFiles();
            WorkWithXml();
            WorkWithConpression();
            WorkWithConpression(useBrotli:false);
        }

        static void OutputFileSystemInfo() {
            WriteLine("{0,-33} {1}", "Path.PathSeparator", PathSeparator);
            WriteLine("{0,-33} {1}", "Path.DirectorySeparatorChar", DirectorySeparatorChar);
            WriteLine("{0,-33} {1}", "Directory.GetCurrentDirectory()", GetCurrentDirectory());
            WriteLine("{0,-33} {1}", "Environment.CurrentDirectory", CurrentDirectory);
            WriteLine("{0,-33} {1}", "Environment.SystemDirectory",  SystemDirectory);
            WriteLine("{0,-33} {1}", "Path.GetTempPath()", GetTempPath());
            WriteLine("GetFolderPath(SpecialFolder");
            WriteLine("{0,-33} {1}", " .System)", 
            GetFolderPath(SpecialFolder.System));
            WriteLine("{0,-33} {1}", " .ApplicationData)",
            GetFolderPath(SpecialFolder.ApplicationData));
            WriteLine("{0,-33} {1}", " .MyDocuments)",
            GetFolderPath(SpecialFolder.MyDocuments));
            WriteLine("{0,-33} {1}", " .Personal)",
            GetFolderPath(SpecialFolder.Personal));
        }

        static void WorkWithDrives() {        
            WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18} | {4,18}",
                "NAME", "TYPE", "FORMAT", "SIZE (BYTES)", "FREE SPACE");
            foreach (DriveInfo drive in DriveInfo.GetDrives()) {
                if (drive.IsReady) {
                    WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18:N0} | {4,18:N0}",
                    drive.Name, drive.DriveType, drive.DriveFormat, drive.TotalSize, drive.AvailableFreeSpace);
                }
                else {
                    WriteLine("{0,-30} | {1,-10}", drive.Name, drive.DriveType);
                }
            }
        }

        static void WorkWithDirectories() {
            var newFolder = Combine(GetFolderPath(SpecialFolder.Personal), "NewFolder");
            WriteLine($"Working with: {newFolder}");
            WriteLine($"Does it exist? {Exists(newFolder)}");
            WriteLine("Creating it...");
            CreateDirectory(newFolder);
            WriteLine($"Does it exist? {Exists(newFolder)}");
            Write("Confirm the directory exists, and then press ENTER: ");
            ReadLine();

            WriteLine("Deleting it...");
            Delete(newFolder, recursive:true);
            WriteLine($"Does it exist? {Exists(newFolder)}");
        }

        static string[] callsigns = new string[] { 
            "Husker", "Starbuck", "Apollo", "Boomer", 
            "Bulldog", "Athena", "Helo", "Racetrack" };

        static void workWithFiles() {
            var dir = Combine(GetFolderPath(SpecialFolder.Personal), "NewFolder");
            CreateDirectory(dir);
            string textFile = Combine(dir, "Dummy.txt");
            string backupFile = Combine(dir, "Dummy.bak");

            WriteLine($"Working with {textFile}");
            WriteLine($"Does it exist? {File.Exists(textFile)}");
            StreamWriter textWriter = File.CreateText(textFile);
            textWriter.WriteLine("Hello C#");
            textWriter.Close();

            WriteLine($"Does it exist? {File.Exists(textFile)}");
            File.Copy(sourceFileName:textFile, destFileName:backupFile, overwrite:true);
            WriteLine($"Does {backupFile} exist? {File.Exists(backupFile)}");
            Write("Confirm the files exist, and then press ENTER: ");
            ReadLine();

            File.Delete(textFile);
            WriteLine($"Does it exist? {File.Exists(textFile)}");

            WriteLine($"Reading contents of {backupFile}:");
            StreamReader textReader = File.OpenText(backupFile);
            WriteLine(textReader.ReadToEnd());
            textReader.Close();
            WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
            WriteLine($"File Name: {GetFileName(textFile)}");
            WriteLine("File Name without Extension: {0}", GetFileNameWithoutExtension(textFile));
            WriteLine($"File Extension: {GetExtension(textFile)}");
            WriteLine($"Random File Name: {GetRandomFileName()}");
            WriteLine($"Temporary File Name: {GetTempFileName()}");

            var info = new FileInfo(backupFile);
            WriteLine($"{backupFile}:");
            WriteLine($"Contains {info.Length} bytes");
            WriteLine($"Last accessed {info.LastAccessTime}");
            WriteLine($"Has readonly set to {info.IsReadOnly}");
        }

        static void WorkWithXml() {
            FileStream xmlFileStream = null;
            XmlWriter xml = null;
            try {
            string xmlFile = Combine(CurrentDirectory, "streams.xml");
            xmlFileStream = File.Create(xmlFile);
            xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings{Indent = true});
            xml.WriteStartDocument();
            xml.WriteStartElement("callsigns");
            foreach (string item in callsigns)
            {
                xml.WriteElementString("callsigns", item);
            }
            xml.WriteEndElement();
            xml.Close();
            xmlFileStream.Close();

            WriteLine("{0} contains {1:N0} bytes.",
            arg0:xmlFile,
            arg1:new FileInfo(xmlFile).Length);
            
            WriteLine(File.ReadAllText(xmlFile));
            } catch (Exception ex) {
                WriteLine($"{ex.GetType()} says {ex.Message}");
            }
            finally {
                if (xml!=null) {
                    xml.Dispose();
                    WriteLine("The XML writer's unmanaged resources have been disposed.");
                }
                if (xmlFileStream!=null) {
                    xmlFileStream.Dispose();
                    WriteLine("The file stream's unmanaged resouces have been disposed.");
                }
            }
        }

        static void WorkWithConpression(bool useBrotli = true) {
            string fileExt = useBrotli?"brotli":"gzip";
            string gzipFilePath = Combine(CurrentDirectory, "streams.{fileExt}}");
            FileStream file = File.Create(gzipFilePath);

            Stream compressor;
            if (useBrotli) {
                compressor = new BrotliStream(file, CompressionMode.Compress);
            }
            else {
                compressor = new GZipStream(file, CompressionMode.Compress);
            }
            using (compressor) {
                using (XmlWriter xmlGzip = XmlWriter.Create(compressor)) {
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");
                    foreach (string item in callsigns) {
                        xmlGzip.WriteElementString("callsigns", item);
                    }
                }
            }

            WriteLine("{0} contains {1:N0} bytes.",
                gzipFilePath, new FileInfo(gzipFilePath).Length);
            WriteLine($"The compressed contents:");
            WriteLine(File.ReadAllText(gzipFilePath));

            WriteLine("Reading the compressed XML file:");
                file = File.Open(gzipFilePath, FileMode.Open);

            Stream decompressor;
            if (useBrotli) {
                decompressor = new BrotliStream(file, CompressionMode.Decompress);
            }
            else {
                decompressor = new GZipStream(file, CompressionMode.Decompress);
            }
                using (decompressor) {
                    using (XmlReader reader = XmlReader.Create(decompressor)) {
                        while (reader.Read()) {
                            if ((reader.NodeType == XmlNodeType.Element)&&(reader.Name == "callsign")) {
                                reader.Read();
                                WriteLine($"{reader.Value}");
                            }
                        }
                    }
                }
        }
    }
}
