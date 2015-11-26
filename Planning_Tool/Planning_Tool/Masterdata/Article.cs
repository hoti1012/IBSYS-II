using Planning_Tool.Core;
using System;


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

        public BOM createBom()
        {
            return BOMFactory.create(typeof(BOM),this.article) as BOM;
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

    }

}
