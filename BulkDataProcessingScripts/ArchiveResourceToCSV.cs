using System;
using System.IO;
using System.Text;

namespace BulkDataProcessingScripts
{
    public class ArchiveResourceToCSV
    {
        public ArchiveResourceToCSV(string rootFolder)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Archive Name, Sub Folder Name, File Name, Description");

            int resourceCount = 0;

            foreach (var file in Directory.GetDirectories(rootFolder))
            {
                string archiveName = Path.GetFileName(file);
                output.AppendFormat("{0}, , , \n", archiveName);

                // Iterate through all of the sub-folders within the archive
                foreach(var subFolder in Directory.GetDirectories(file))
                {
                    string folderName = Path.GetFileName(subFolder);
                    output.AppendFormat(", {0}, ,\n", folderName);

                    // Iterate througha ll of the files within this sub-folder
                    foreach(var subFile in Directory.GetFiles(subFolder))
                    {
                        string fileName = Path.GetFileName(subFile);
                        output.AppendFormat(", ,{2}, \n", archiveName, folderName, fileName);
                        resourceCount++;
                    }
                }

                // Check for any loose files inside the archive as well.
                foreach(var subFile in Directory.GetFiles(file))
                {
                    string fileName = Path.GetFileName(subFile);
                    output.AppendFormat(", ,{1}, \n", archiveName, fileName);
                }
            }

            string rootFolderName = Path.GetFileName(rootFolder);
            File.WriteAllText(rootFolderName + "_resources.csv", output.ToString());

            Console.WriteLine("Exported {0} Resources to CSV.", resourceCount);
            Console.ReadLine();
        }
    }
}
