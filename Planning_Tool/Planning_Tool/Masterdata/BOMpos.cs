using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    class BOMpos
    {
        /// <summary>
        /// Stücklistennummer
        /// </summary>
        private string _bom;

        /// <summary>
        /// Artikelnummer
        /// </summary>
        private string _bompos;

        /// <summary>
        /// Menge
        /// </summary>
        private int _amount;

        public void update()
        {
            BOMposFactory.Update(this);
        }

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string BOMpos
        {
            get { return _bompos; }
            set { _bompos = value; }
        }

        public string bom
        {
            get { return _bom; }
            set { _bom = value; }
        }
    }
}
