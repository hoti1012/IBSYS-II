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
            set { _orderBOMpos = value; }
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

        private int _amountN1;

        public int amountN1
        {
            get { return _amountN1; }
            set { _amountN1 = value; }
        }

        private int _amountN2;

        public int amountN2
        {
            get { return _amountN2; }
            set { _amountN2 = value; }
        }

        private int _amountN3;

        public int amountN3
        {
            get { return _amountN3; }
            set { _amountN3 = value; }
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
    }
}
