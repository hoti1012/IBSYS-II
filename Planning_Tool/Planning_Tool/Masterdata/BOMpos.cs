using Planning_Tool.Core;
using Planning_Tool.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    public class BOMpos : PlanningPosObject
    {
        public static string TABLE = typeof(BOMpos).Name;

        /// <summary>
        /// Stücklistennummer
        /// </summary>
        private string _bom;

        /// <summary>
        /// Artikelnummer
        /// </summary>
        private string _bompos;

        /// <summary>
        /// Artikelbezeichnung
        /// </summary>
        private string _designation;

        /// <summary>
        /// Menge
        /// </summary>
        private int _amount;

        /// <summary>
        /// Gibt an ob die Stücklistenposition eine Baugruppe ist
        /// </summary>
        /// <returns>true wenn bompos eine Baugruppe ist</returns>
        public bool isModule()
        {
            if (BOMFactory.search(typeof(BOM), this._bompos) != null)
                return true;
            return false;
        }

        /// <summary>
        /// gibt an ob die Bompos noch unterbaugruppen enthält
        /// </summary>
        /// <returns></returns>
        public bool hasModule()
        {
            BOMpos pos = null;
            if (isModule())
            {
                foreach(PlanningPosObject o in BOMposFactory.search(typeof(BOMpos),this._bompos))
                {
                    pos = o as BOMpos;
                    if (pos.isModule())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string bom
        {
            get { return _bom; }
            set { _bom = value; }
        }

        public string bompos
        {
            get { return _bompos; }
            set {
                if(value != null && !value.Equals(_bompos)){
                    Article art = ArticleFactory.search(typeof(Article),value) as Article;
                    if (art == null)
                        throw new ArticleNotFoundException(value);
                    _bompos = value;
                    _designation = art.Designation;
                }
            }
        }

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

    }
}
