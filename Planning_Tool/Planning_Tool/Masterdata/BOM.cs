using Planning_Tool.Core;
using Planning_Tool.Exceptions;
using System;
using System.Collections.Generic;

namespace Planning_Tool.Masterdata
{
    public class BOM : PlanningObject
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
            return BOMposFactory.create(typeof(BOMpos),this.bom,article) as BOMpos;
        }

        public string bom
        {
            get { return _bom; }
            set
            {

                if (value != null)
                {
                    Article art = ArticleFactory.search(typeof(Article), value) as Article;
                    if (art == null)
                        throw new ArticleNotFoundException(value);

                    _bom = value;
                    _designation = art.Designation;

                }
            }
        }

        /// <summary>
        /// gibt an ob die Bompos noch unterbaugruppen enthält
        /// </summary>
        /// <returns></returns>
        public bool hasModule()
        {
            BOMpos pos = null;
            foreach (PlanningPosObject o in BOMposFactory.search(typeof(BOMpos), this._bom))
            {
                pos = o as BOMpos;
                if (pos.isModule())
                {
                    return true;
                }
            }
            return false;
        }

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }
    }

}