using Planning_Tool.Core;
using Planning_Tool.Exceptions;
using Planning_Tool.Forecasts;
using Planning_Tool.Masterdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    class ProductionPlan : PlanningObject
    {
        /// <summary>
        /// Enthält die Artikelnummer
        /// </summary>
        private string _productionPlan;

        /// <summary>
        /// Bezeichnung
        /// </summary>
        private string _designation;

        /// <summary>
        /// gibt an wie oft der Artikel Produziert wird
        /// </summary>
        private int _amount;

        /// <summary>
        /// Gibt die Reihenfolge an
        /// </summary>
        private int _position;

        public int position
        {
            get { return _position; }
            set { _position = value; }
        }

        public string productionPlan
        {
            get { return _productionPlan; }
            set { 
                
                _productionPlan = value;
                Article art = ArticleFactory.search(typeof(Article),value) as Article;
                if (art == null)
                {
                    throw new ArticleNotFoundException(value);
                }
                this._designation = art.Designation;
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
