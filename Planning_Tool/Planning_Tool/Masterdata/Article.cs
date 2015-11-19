using System;


namespace Planning_Tool.Masterdata
{
    /// <summary>
    /// Class Article
    /// </summary>
    public class Article
    {
        public static string TABLE = "ARTICLE";

        /// <summary>
        /// Artikelnummer
        /// </summary>
        private string articleNumber;

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

        public double OrderPriceExpress
        {
            get { return orderPriceExpress; }
            set { orderPriceExpress = value; }
        }

        public double OrderPriceNormal
        {
            get { return orderPriceNormal; }
            set { orderPriceNormal = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public bool IsProduction
        {
            get { return isProduction; }
            set { isProduction = value; }
        }

        public bool IsPurchase
        {
            get { return isPurchase; }
            set { isPurchase = value; }
        }

        public string ArticleNumber
        {
            get { return articleNumber; }
            set { articleNumber = value; }
        }

        public string Designation
        {
            get { return designation; }
            set { designation = value; }
        }
    }

}
