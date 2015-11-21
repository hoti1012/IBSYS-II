using System;

namespace Planning_Tool.Masterdata
{
    public class BOM
    {
        public static string TABLE = "BOM";

        /// <summary>
        /// Stücklisten nummer (ist die Artikelnummer der E-Artikels)
        /// </summary>
        private string _bom;

        public string bom
        {
            get { return _bom; }
            set { _bom = value; }
        }
    }

}