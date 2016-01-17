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
using Planning_Tool.Forecasts;
using Planning_Tool.Core;

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
                periodObj = PeriodFactory.create(typeof(Period), (Convert.ToInt32(_root.Attributes["period"].Value)+1).ToString()) as Period;
                
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
        public static void write(string path)
        {
            try
            {
                if (path == null)
                {
                    throw new Exception("Bitte geben Sie einen Pfad an!");
                }

                XmlDocument doc = new XmlDocument();
                XmlNode root, nroot, node;
                XmlAttribute attribute;

                //Grundstruktur 
                root = doc.CreateElement("input");
                doc.AppendChild(root);

                node = doc.CreateElement("qualitycontrol");

                attribute = doc.CreateAttribute("type");
                attribute.InnerText = "no";
                node.Attributes.Append(attribute);

                root.AppendChild(node);

                attribute = doc.CreateAttribute("losequantity");
                attribute.InnerText = "0";
                node.Attributes.Append(attribute);

                root.AppendChild(node);

                attribute = doc.CreateAttribute("delay");
                attribute.InnerText = "0";
                node.Attributes.Append(attribute);

                root.AppendChild(node);            

                //Verkaufsaufträge
                nroot = doc.CreateElement("sellwish");
                root.AppendChild(nroot);

                foreach(Forecast f in ForecastFactory.getAll())
                {
                    node = doc.CreateElement("item");

                    attribute = doc.CreateAttribute("article");
                    attribute.InnerText = f.forecast;
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    attribute = doc.CreateAttribute("quantity");
                    attribute.InnerText = f.currentAmount.ToString();
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);
                }

                //Direktverkäufe
                List<PlanningObject> selldirect = DirektSaleFactory.searchAll(typeof(DirektSale));
                if (selldirect == null || selldirect.Count < 3)
                {
                    if (selldirect == null)
                    {
                        selldirect = new List<PlanningObject>();
                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        DirektSale dsObj = DirektSaleFactory.search(typeof(DirektSale), i.ToString()) as DirektSale;
                        if (dsObj == null)
                        {
                            dsObj = DirektSaleFactory.create(typeof(DirektSale), i.ToString()) as DirektSale;
                            dsObj.amount = 0;
                            dsObj.penalty = 0.0;
                            dsObj.price = 0.0;
                            dsObj.update();
                            selldirect.Add(dsObj);
                        }
                    }
                }

                nroot = doc.CreateElement("selldirect");
                root.AppendChild(nroot);

                foreach(DirektSale d in selldirect)
                {
                    node = doc.CreateElement("item");

                    attribute = doc.CreateAttribute("article");
                    attribute.InnerText = d.direktSale;
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    attribute = doc.CreateAttribute("quantity");
                    attribute.InnerText = d.amount.ToString();
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    attribute = doc.CreateAttribute("price");
                    attribute.InnerText = d.price.ToString();
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    attribute = doc.CreateAttribute("penalty");
                    attribute.InnerText = d.penalty.ToString();
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);
                }

                //Bestellungen
                nroot = doc.CreateElement("orderlist");
                root.AppendChild(nroot);

                foreach(OrderingPos o in OrderingPosFactory.getAllCurrentOrder())
                {
                    node = doc.CreateElement("order");

                    attribute = doc.CreateAttribute("article");
                    attribute.InnerText = o.orderingpos;
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    attribute = doc.CreateAttribute("quantity");
                    attribute.InnerText = o.Amount.ToString();
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    string modus = null;
                    if (o.IsExpress)
                    {
                        modus = "5";
                    }
                    else
                    {
                        modus = "4";
                    }
                    attribute = doc.CreateAttribute("modus");
                    attribute.InnerText = modus;
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);
                }

                //Produktionsaufträge
                nroot = doc.CreateElement("productionlist");
                root.AppendChild(nroot);

                foreach (ProductionPlan pp in ProductionPlanFactory.getOrderdProductionPlan())
                {
                    node = doc.CreateElement("production");

                    attribute = doc.CreateAttribute("article");
                    attribute.InnerText = pp.productionPlan;
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    attribute = doc.CreateAttribute("quantity");
                    attribute.InnerText = pp.amount.ToString();
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);
                }


                //Capazitätsplanung
                nroot = doc.CreateElement("workingtimelist");
                root.AppendChild(nroot);

                foreach (CapacityPlan cp in CapacityPlanFactory.searchAll(typeof(CapacityPlan)))
                {
                    node = doc.CreateElement("workingtime");

                    attribute = doc.CreateAttribute("station");
                    attribute.InnerText = cp.capacityPlan;
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    attribute = doc.CreateAttribute("shift");
                    attribute.InnerText = cp.shift.ToString();
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);

                    attribute = doc.CreateAttribute("overtime");
                    attribute.InnerText = cp.overTime.ToString();
                    node.Attributes.Append(attribute);

                    nroot.AppendChild(node);
                }

                doc.Save(path);
            }
            finally
            {
                //nichts tun
            }
        }
    }
}
