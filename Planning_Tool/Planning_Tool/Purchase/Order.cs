using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Purchase
{
    class Order
    {
        public static string TABLE = "ORDER";

        /// <summary>
        /// Enthält sämtliche Poisionen
        /// </summary>
        private List<OrderPos> pos;

        /// <summary>
        /// Preis der Bestellung
        /// </summary>
        private double price;

        private int amount;

        private bool express;
    }
}
