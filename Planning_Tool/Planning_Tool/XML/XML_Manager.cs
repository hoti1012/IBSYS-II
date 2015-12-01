using Planning_Tool.Masterdata;
using Planning_Tool.Production;
using Planning_Tool.Purchase;
using Planning_Tool.Time;
using Planning_Tool.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Planning_Tool.XML
{
    class XML_Manager
    {
        /// <summary>
        /// Liest die Ergebnisxml aus
        /// </summary>
        public static void read(string path)
        {
            Stock stockObj;
            Period periodObj;
            Ordering orderObj;
            OrderingPos orderingPosObj;
            Workplace wpObj;
            WorkplacePos wpPosObj;
            XmlDocument _doc = new XmlDocument();
            XmlElement _root = null;
            try
            {
                if (path == null || path == "")
                {
                    throw new NotFoundException("Bitte geben Sie einen Pfad ein");
                }

                _doc.Load(path);

                if (!_doc.HasChildNodes)
                {
                    throw new WrongFormatException("Die XML-Datei ist leer oder im Falschen Format");
                }
                _root = _doc.DocumentElement;

                if (!_root.HasAttributes)
                {
                    throw new WrongFormatException("Die eingelesene XML ist im falschen Format");
                }
                //Aktuelle periode einlesen
                periodObj = PeriodFactory.create(typeof(Period), _root.Attributes["period"].Value) as Period;
                
                //Lagerbestand einlesen 
                foreach (XmlNode node in _root.GetElementsByTagName("warehousestock"))
                {
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        if (!n.Name.Equals("totalstockvalue", StringComparison.CurrentCultureIgnoreCase))
                        {
                            stockObj = StockFactory.create(typeof(Stock), n.Attributes["id"].Value) as Stock;
                            stockObj.amount = Convert.ToInt32(n.Attributes["amount"].Value);
                            stockObj.price = Convert.ToDouble(n.Attributes["price"].Value.Replace(".",","));
                            stockObj.stockvalue = Convert.ToDouble(n.Attributes["stockvalue"].Value.Replace(".",","));
                            stockObj.update();
                        }
                    }
                }

                //Offene Bestellungen einlesen
                foreach (XmlNode node in _root.GetElementsByTagName("futureinwardstockmovement"))
                {
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        orderObj = OrderingFactory.search(typeof(Ordering), n.Attributes["orderperiod"].Value) as Ordering;
                        if (orderObj == null)
                        {
                            orderObj = OrderingFactory.create(typeof(Ordering), n.Attributes["orderperiod"].Value) as Ordering;
                        }
                        orderingPosObj = orderObj.addPos(n.Attributes["article"].Value);
                        orderingPosObj.Amount = Convert.ToInt32(n.Attributes["amount"].Value);
                        if (n.Attributes["mode"].Value != "5")
                        {
                            orderingPosObj.IsExpress = true;
                        }
                        orderingPosObj.IsOrdered = true;
                        orderingPosObj.update();
                    }
                }

                //Offene Warteschlangen einlesen
                foreach (XmlNode node in _root.GetElementsByTagName("waitinglistworkstations"))
                {
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        wpObj = WorkplaceFactory.search(typeof(Workplace),n.Attributes["id"].Value) as Workplace;
                        if (wpObj == null)
                        {
                            wpObj = WorkplaceFactory.create(typeof(Workplace), n.Attributes["id"].Value) as Workplace;
                        }
                        if (n.HasChildNodes)
                        {
                            wpPosObj = null;
                            foreach (XmlNode pos in n.ChildNodes)
                            {
                                wpPosObj = WorkplacePosFactory.search(typeof(WorkplacePos),wpObj.workplace,pos.Attributes["item"].Value) as WorkplacePos;
                                if(wpPosObj == null)
                                {
                                    wpPosObj = wpObj.addPos(pos.Attributes["item"].Value);
                                }
                                wpPosObj.amountWaitlist = Convert.ToInt32(pos.Attributes["amount"].Value);
                                wpPosObj.update();
                            }
                        }
                    }
                }

                wpObj = null;
                //Aufträge in Bearbeitung einlesen
                foreach (XmlNode node in _root.GetElementsByTagName("ordersinwork"))
                {
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        wpObj = WorkplaceFactory.search(typeof(Workplace), n.Attributes["id"].Value) as Workplace;
                        if (wpObj == null)
                        {
                            wpObj = WorkplaceFactory.create(typeof(Workplace), n.Attributes["id"].Value) as Workplace;
                        }
                        wpPosObj = WorkplacePosFactory.search(typeof(WorkplacePos), wpObj.workplace, n.Attributes["item"].Value) as WorkplacePos;
                        if (wpPosObj == null)
                        {
                            wpPosObj = wpObj.addPos(n.Attributes["item"].Value);
                        }
                        wpPosObj.amountInWork = Convert.ToInt32(n.Attributes["amount"].Value);
                        wpPosObj.update();
                    }
                }
            }
            finally
            {
                //nichts tun
            }
        }

        /// <summary>
        /// Schreibt die Inputxml
        /// </summary>
        public void write()
        {
            try
            {
                //TODO: Funktion implementieren
                throw new NotImplementedException("Diese Funktion muss noch implementiert werden");
            }
            finally
            {
                //nichts tun
            }
        }
    }
}
