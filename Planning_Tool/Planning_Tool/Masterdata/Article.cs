using System;


namespace Planning_Tool.Masterdata
{
    /// <summary>
    /// Class Article
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Artikelnummer
        /// </summary>
        private string articleNumber;
        
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
        /// Stückliste des Artikels
        /// </summary>
        private BOM bom;

        /// <summary>
        /// Arbeitsplan des Artikels
        /// </summary>
        private WorkSchedule ws;


        public Article()
        {
        }
    }

}
