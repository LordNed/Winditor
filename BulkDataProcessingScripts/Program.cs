using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataProcessingScripts
{
    class Program
    {
        static void Main(string[] args)
        {
            //new ArchiveResourceToCSV(@"E:\New_Data_Drive\WindwakerModding\new_object_extract\res\extracted_archives\Object");
            new ArcExtractorDebugging(@"E:\New_Data_Drive\WindwakerModding\root\res\Object\ff.arc");
        }
    }
}
