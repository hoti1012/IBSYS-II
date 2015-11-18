using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    class ArticleFactory
    {
        /// <summary>
        /// Erstellt einen neuen Artikel
        /// </summary>
        /// <returns>Gibt den neu erzeugten Artikel zurück</returns>
        public Article create()
        {
            return new Article();
        }

        /// <summary>
        /// Sucht einen Artikel anhand einer Artikelnummer
        /// </summary>
        /// <param name="articleNumber">Artikelnummer des Gesuchten Artikels</param>
        /// <returns></returns>
        public Article search(string articleNumber)
        {
            return new Article();
        }
    }
}
