using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFilesInFolders
{
    class FilePOCO
    {

        public string name { get; set; }
        public string directory { get; set; }
        public string hash { get; set; }

        public FilePOCO(string name, string directory, string hash)
        {
            this.name = name;
            this.directory = directory;
            this.hash = hash;
        }
    }
}
