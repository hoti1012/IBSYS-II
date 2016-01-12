using Planning_Tool.Core;
using Planning_Tool.Production;
using System;
using System.Collections.Generic;


namespace Planning_Tool.Masterdata
{
    /// <summary>
    /// Class Article
    /// </summary>
    public class Article : PlanningObject
    {
        public static string TABLE = typeof(Article).Name;

        /// <summary>
        /// Artikelnummer
        /// </summary>
        private string _article;

        /// <summary>
        /// Bezeichnung
        /// </summary>
        private string designation;

        /// <summary>
        /// Kenner ob Artikel ein Einkaufsteil ist
        /// </summary>
        private bool isPurchase;

        /// <summary>
        /// Kenner ob Artikel ein Fertigungsteil ist
        /// </summary>
        private bool isProduction;

        /// <summary>
        /// Einkaufspreis (Stück)
        /// </summary>
        private double price;

        /// <summary>
        /// Bestellpreis für eine Normale Bestellung
        /// </summary>
        private double orderPriceNormal;

        /// <summary>
        /// Bestellpreis für eine Express Bestellung
        /// </summary>
        private double orderPriceExpress;

        /// <summary>
        /// Enthält die normale Lieferzeit
        /// </summary>
        private double deliverTime;

        /// <summary>
        /// Enthält die express Lieferzeit
        /// </summary>
        private double deliverTimeExpress;

        /// <summary>
        /// Enthält die Abweichung bei Normallieferungen
        /// </summary>
        private double diliverDeviation;

        /// <summary>
        /// Gibt die Menge an ab der es 10% rabatt gibt
        /// </summary>
        private int discount;

        /// <summary>
        /// Sicherheitsbestand
        /// </summary>
        private int _safetyStock;

        /// <summary>
        /// Gibt an wie oft ein Artikel verwendet wird
        /// </summary>
        private int _use;

        public int use
        {
            get { return _use; }
            set { _use = value; }
        }

        /// <summary>
        /// Gibt eine Liste zurück in wecher die Artikelnummern sind in dem dieser Artikel verwendet wird
        /// </summary>
        /// <returns></returns>
        public List<string> getUse()
        {
            List<string> res = new List<string>();
            foreach (Article art in Article.getAllMainArticle())
            {
                foreach (BOMpos pos in art.getAllBomPos())
                {
                    if (pos.bompos.Equals(this._article, StringComparison.CurrentCultureIgnoreCase))
                    {
                        res.Add(art._article);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Gibt den Lagerbestand zurück
        /// </summary>
        /// <returns></returns>
        public int getStockAmount()
        {
            int res = 0;
            Stock stock = null;

            stock = StockFactory.search(typeof(Stock),this.article) as Stock;
            if (stock != null)
            {
                res = stock.amount;
            }
            return res;
        }

        /// <summary>
        /// Gibt die anzahl der in Arbeit zu diesem Artikel zurück
        /// </summary>
        /// <returns></returns>
        public int getInWork()
        {
            int res = 0;
            WorkplacePos wpPosObj = null;
            List<PlanningPosObject> wpList = new List<PlanningPosObject>();
            wpList = WorkplacePosFactory.searchAllWithPos(typeof(WorkplacePos),this.article);
            foreach (PlanningPosObject wp in wpList)
            {
                wpPosObj = wp as WorkplacePos;
                res += wpPosObj.amountInWork;
            }
            return res;
        }

        /// <summary>
        /// Gibt die Warteschlage zu diesem Artikel zurück
        /// </summary>
        /// <returns></returns>
        public int getWaitingList()
        {
            int res = 0;
            WorkplacePos wpPosObj = null;
            List<PlanningPosObject> wpList = new List<PlanningPosObject>();
            wpList = WorkplacePosFactory.searchAllWithPos(typeof(WorkplacePos), this.article);
            foreach (PlanningPosObject wp in wpList)
            {
                wpPosObj = wp as WorkplacePos;
                res += wpPosObj.amountWaitlist;
            }
            return res;
        }

        /// <summary>
        /// Gibt alle Stücklistenpositionen zu diesem Artikel zurück
        /// </summary>
        public List<BOMpos> getAllBomPos()
        {
            List<BOMpos> bompos = new List<BOMpos>();
            ArticleFactory.getAllBomPos(this._article, bompos);
            return bompos;
        }

        /// <summary>
        /// Gibt alle Baugruppen zu diesem Artikel zurück
        /// </summary>
        public List<BOMpos> getAllModule()
        {
            List<BOMpos> bompos = new List<BOMpos>();
            ArticleFactory.getAllModule(this._article, bompos);
            return bompos;
        }

        /// <summary>
        /// Gibt alle direkten Baugruppen zu diesem Artikel zurück
        /// </summary>
        /// <returns></returns>
        public List<BOMpos> getModule()
        {
            List<BOMpos> bompos = new List<BOMpos>();
            ArticleFactory.getModule(this._article, bompos);
            return bompos;
        }

        /// <summary>
        /// Erzeugt einen Stücklistenkopf
        /// </summary>
        /// <returns></returns>
        public BOM createBom()
        {
            return BOMFactory.create(typeof(BOM),this.article) as BOM;
        }

        public static List<Article> getAllMainArticle()
        {
            string sql = "Select * from " + typeof(Article).Name;
            sql += " WHERE NOT EXISTS (SELECT * FROM " + typeof(BOMpos).Name + " WHERE " + typeof(BOMpos).Name + " = " + typeof(Article).Name + ")";
            List<PlanningObject> tmp = null;
            List<Article> artList = new List<Article>();
            tmp = ArticleFactory.select(typeof(Article),sql);
            foreach (PlanningObject o in tmp)
            {
                artList.Add(o as Article);
            }
            return artList;
        }

        public string article
        {
            get { return _article; }
            set
            {
                if (_article == null)
                    _article = value;
            }
        }

        public string Designation
        {
            get { return designation; }
            set { designation = value; }
        }

        public bool IsProduction
        {
            get { return isProduction; }
            set { isProduction = value;
            if (value == true)
            {
                isPurchase = false;
            }                  
            }
        }

        public bool IsPurchase
        {
            get { return isPurchase; }
            set { isPurchase = value;
            if (value == true)
            {
                isProduction = false;
            }     
            }
        }
        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public double DiliverDeviation
        {
            get { return diliverDeviation; }
            set { diliverDeviation = value; }
        }

        public double DeliverTimeExpress
        {
            get { return deliverTimeExpress; }
            set { deliverTimeExpress = value; }
        }

        public double DeliverTime
        {
            get { return deliverTime; }
            set {
                if (value > 0)
                {
                    deliverTimeExpress = Math.Round(value / 2,1);
                    deliverTime = value;
                }
            }
        }

        public double OrderPriceExpress
        {
            get { return orderPriceExpress; }
            set { orderPriceExpress = value; }
        }

        public double OrderPriceNormal
        {
            get { return orderPriceNormal; }
            set {
                if (value > 0)
                {
                    orderPriceNormal = value;
                    orderPriceExpress = value * 10;
                }
            }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int safetyStock
        {
            get { return _safetyStock; }
            set { _safetyStock = value; }
        }
    }

}
