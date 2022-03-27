using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFilesInFolders
{
    class Fichier
    {

        public string nom { get; set; }
        public string repertoire { get; set; }
        public string repertoireCourt { get; set; }
        public string hash { get; set; }

        public Fichier(string nom, string repertoire, string repertoireCourt, string hash)
        {
            this.nom = nom;
            this.repertoire = repertoire;
            this.repertoireCourt = repertoireCourt;
            this.hash = hash;
        }

        override
        public string ToString()
        {
            return "repertoire: " + repertoire + " - repertoireCourt: " + repertoireCourt + " - nom: " + nom + " - hash: " + hash;
        }
    }
}
