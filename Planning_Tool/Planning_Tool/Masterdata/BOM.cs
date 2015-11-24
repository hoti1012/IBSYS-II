using System;

namespace Planning_Tool.Masterdata
{
    public class BOM
    {
        public static string TABLE = typeof(BOMpos).Name;

        /// <summary>
        /// Stücklisten nummer (ist die Artikelnummer der E-Artikels)
        /// </summary>
        private string _bom;

        /// <summary>
        /// Bezeichnung des Artikels
        /// </summary>
        private string _designation;

        /// <summary>
        /// fügt eine Stücklistenposition hinzu
        /// </summary>
        /// <param name="article">Artikelnummer</param>
        /// <returns>Stücklistenposition</returns>
        public BOMpos addPos(string article)
        {
            return BOMposFactory.create(this.bom,article);
        }

        public string bom
        {
            get { return _bom; }
            set { _bom = value; }
        }

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }
    }

}