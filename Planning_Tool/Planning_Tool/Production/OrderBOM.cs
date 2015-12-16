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
    class OrderBOM : PlanningObject
    {
        private string _orderBOM;

        public string orderBOM
        {
            get { return _orderBOM; }
            set 
            {
                Article art = ArticleFactory.search(typeof(Article), value) as Article;
                if (art == null)
                {
                    throw new ArticleNotFoundException(value);
                }

                _orderBOM = value;
                _designation = art.Designation;
            }
        }

        private string _designation;

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        private int _sellwish;

        public int sellwisch
        {
            get { return _sellwish; }
            set { _sellwish = value; }
        }
    }
}
