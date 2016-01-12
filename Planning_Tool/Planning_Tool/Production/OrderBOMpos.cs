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

        private string _dependence;

        public string dependence
        {
            get { return _dependence; }
            set { _dependence = value; }
        }

        private int _amount;

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private bool _isBom;

        public bool isBom
        {
            get { return _isBom; }
            set { _isBom = value; }
        }

        private bool _isExplode;

        public bool isExplode
        {
            get { return _isExplode; }
            set { _isExplode = value; }
        }

        /// <summary>
        /// Löst eine Baugruppe innerhalb einer Stückliste auf
        /// </summary>
        internal void explode()
        {
            List<PlanningPosObject> list = PlanningPosObjectFactory.search(typeof(BOMpos), this.orderBOMpos);
            foreach (PlanningPosObject p in list)
            {
                BOMpos bom = p as BOMpos;
                OrderBOMpos oBom = OrderBOMposFactory.create(typeof(OrderBOMpos), this.orderBOM, bom.bompos, orderBOMpos) as OrderBOMpos;
                Stock stock = StockFactory.search(typeof(Stock), bom.bompos) as Stock;
                Article art = ArticleFactory.search(typeof(Article), bom.bompos) as Article;
                if (art.IsProduction)
                {
                    int use = art.use;
                    if (use <= 0)
                        use = 1;
                    int amount = (this.amount * bom.amount + (stock.safetyStock / use)) - (art.getWaitingList() / use) - (art.getInWork() / use);
                    if (amount < 0)
                        amount = 0;

                    oBom.isBom = bom.isModule();
                    oBom.amount = amount;
                    oBom.isExplode = false;
                    oBom.update();
                }
                else
                {
                    int amount = this.amount * bom.amount;
                    if (amount < 0)
                        amount = 0;

                    oBom.isBom = false;
                    oBom.amount = amount;
                    oBom.isExplode = false;
                    oBom.update();
                }
            }
            this.isExplode = true;
            update();
        }
    }
}
