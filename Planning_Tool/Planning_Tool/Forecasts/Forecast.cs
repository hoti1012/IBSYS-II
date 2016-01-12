using Planning_Tool.Core;
using Planning_Tool.Exceptions;
using Planning_Tool.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Forecasts
{
    class Forecast : PlanningObject
    {
        /// <summary>
        /// Enthält die Artikelnummer
        /// </summary>
        private string _forecast;

        /// <summary>
        /// Gibt die Anzahl der Aktuellen Aufträge zurück
        /// </summary>
        private int _currentAmount;

        /// <summary>
        /// Gibt die Prognose der P + 1
        /// </summary>
        private int _amountOne;

        /// <summary>
        /// Gibt die Prognose der P + 2
        /// </summary>
        private int _amountTwo;

        /// <summary>
        /// Gibt die Prognose der P + 3
        /// </summary>
        private int _amountThree;

        /// <summary>
        /// Speichert die Eingetragenen Prognosen in der Datenbank
        /// </summary>
        /// <param name="a10"></param>
        /// <param name="a11"></param>
        /// <param name="a12"></param>
        /// <param name="a13"></param>
        /// <param name="a20"></param>
        /// <param name="a21"></param>
        /// <param name="a22"></param>
        /// <param name="a23"></param>
        /// <param name="a30"></param>
        /// <param name="a31"></param>
        /// <param name="a32"></param>
        /// <param name="a33"></param>
        public static void saveForecasts(int a10, int a11, int a12, int a13, 
                                    int a20, int a21, int a22, int a23,
                                    int a30, int a31, int a32, int a33)
        {
            Forecast a1 = null;
            Forecast a2 = null;
            Forecast a3 = null;

            if (a10 == 0 || a20 == 0 || a30 == 0)
            {
                throw new NeedMoreInformationsException("Es müssen mindestens die Aufträge für die Aktuelle Periode eingetragen werden");
            }

            a1 = ForecastFactory.search(typeof(Forecast), "1") as Forecast;
            if (a1 == null)
            {
                a1 = ForecastFactory.create(typeof(Forecast), "1") as Forecast;
            }

            a2 = ForecastFactory.search(typeof(Forecast), "2") as Forecast;
            if (a2 == null)
            {
                a2 = ForecastFactory.create(typeof(Forecast), "2") as Forecast;
            }

            a3 = ForecastFactory.search(typeof(Forecast), "3") as Forecast;
            if (a3 == null)
            {
                a3 = ForecastFactory.create(typeof(Forecast), "3") as Forecast;
            }

            try
            {
                a1.currentAmount = a10;
                a2.currentAmount = a20;
                a3.currentAmount = a30;

                if(a11 == 0 || a12 == 0 || a13 == 0
                    || a21 == 0 || a22 == 0 || a23 == 0
                    || a31 == 0 || a32 == 0 || a33 == 0){
                    throw new NeedMoreInformationsException("Bestmögliche Planungergenisse erhalten Sie nur wenn auch sämtliche Prognosen ausgefüllt sind");
                }

                a1.amountOne = a11;
                a1.amountTwo = a12;
                a1.amountThree = a13;

                a2.amountOne = a21;
                a2.amountTwo = a22;
                a2.amountThree = a23;

                a3.amountOne = a31;
                a3.amountTwo = a32;
                a3.amountThree = a33;
            }
            finally
            {
                a1.update();
                a2.update();
                a3.update();
                //ProductionPlan.setForecasts();
            }
        }

        public string forecast
        {
            get { return _forecast; }
            set { _forecast = value; }
        }

        public int currentAmount
        {
            get { return _currentAmount; }
            set { _currentAmount = value; }
        }

        public int amountOne
        {
            get { return _amountOne; }
            set { _amountOne = value; }
        }


        public int amountTwo
        {
            get { return _amountTwo; }
            set { _amountTwo = value; }
        }

        public int amountThree
        {
            get { return _amountThree; }
            set { _amountThree = value; }
        }
    }
}
