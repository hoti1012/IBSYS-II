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
        /// Speichert sämtliche änderungen in die Datenbank
        /// </summary>
        public void update()
        {
            ArticleFactory.update(this);
        }

        public BOM createBom()
        {
            return BOMFactory.create(this.article);
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
            set { deliverTime = value; }
        }

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

        public string article
        {
            get { return _article; }
            set { //Artikelnummer soll nicht mehr geändert werden können 
                if(_article == null)
                    _article = value; 
                }
        }

        public string Designation
        {
            get { return designation; }
            set { designation = value; }
        }
    }

}
