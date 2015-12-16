using Planning_Tool.Core;
using Planning_Tool.Exceptions;
using Planning_Tool.Masterdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    class OrderBOMpos : PlanningPosObject
    {
        private string _orderBOM;

        public string orderBOM
        {
            get { return _orderBOM; }
            set { _orderBOM = value; }
        }

        private string _orderBOMpos;

        public string orderBOMpos
        {
            get { return _orderBOMpos; }
            set 
            {
                Article art = ArticleFactory.search(typeof(Article), value) as Article;
                if (art == null)
                    throw new ArticleNotFoundException(value);
                _orderBOMpos = value;
                _designation = art.Designation;
            }
        }

        private string _designation;

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        private int _production;

        public int production
        {
            get { return _production; }
            set { _production = value; }
        }
    }
}
