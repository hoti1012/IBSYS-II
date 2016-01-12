using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Masterdata;
using Planning_Tool.Production;
using Planning_Tool.Purchase;

namespace Planning_Tool.Data
{
    public class FillTable
    {
        public static void createMasterdata()
        {
            Type type = typeof(Article);
            Article art = null;
            BOM bom = null;
            BOMpos bomPos = null;

            //Kaufteile
            art = ArticleFactory.create(type, "21") as Article;
            if (art != null)
            {
                art.Designation = "Kette";
                art.IsPurchase = true;
                art.Price = 5;
                art.OrderPriceNormal = 50.00;
                art.Discount = 300;
                art.DeliverTime = 1.8;
                art.DiliverDeviation = 0.4;
                art.update();
            }

            art = ArticleFactory.create(type, "22") as Article;
            if (art != null)
            {
                art.Designation = "Kette";
                art.IsPurchase = true;
                art.Price = 6.50;
                art.OrderPriceNormal = 50.00;
                art.Discount = 300;
                art.DeliverTime = 1.7;
                art.DiliverDeviation = 0.4;
                art.update();
            }

            art = ArticleFactory.create(type, "23") as Article;
            if (art != null)
            {
                art.Designation = "Kette";
                art.IsPurchase = true;
                art.Price = 6.50;
                art.OrderPriceNormal = 50.00;
                art.Discount = 300;
                art.DeliverTime = 1.2;
                art.DiliverDeviation = 0.2;
                art.update();
            }

            art = ArticleFactory.create(type, "24") as Article;
            if (art != null)
            {
                art.Designation = "Mutter 3/8";
                art.IsPurchase = true;
                art.Price = 0.06;
                art.Discount = 6100;
                art.OrderPriceNormal = 100.00;
                art.DeliverTime = 3.2;
                art.DiliverDeviation = 0.3;
                art.update();
            }

            art = ArticleFactory.create(type, "25") as Article;
            if (art != null)
            {
                art.Designation = "Scheibe 3/8";
                art.IsPurchase = true;
                art.Price = 0.06;
                art.Discount = 3600;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 0.9;
                art.DiliverDeviation = 0.2;
                art.update();
            }

            art = ArticleFactory.create(type, "27") as Article;
            if (art != null)
            {
                art.Designation = "Schraube 3/8";
                art.IsPurchase = true;
                art.Price = 0.10;
                art.Discount = 1800;
                art.OrderPriceNormal = 75.00;
                art.DeliverTime = 0.9;
                art.DiliverDeviation = 0.2;
                art.update();
            }

            art = ArticleFactory.create(type, "28") as Article;
            if (art != null)
            {
                art.Designation = "Rohr 3/4";
                art.IsPurchase = true;
                art.Price = 1.20;
                art.Discount = 4500;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.7;
                art.DiliverDeviation = 0.4;
                art.update();
            }

            art = ArticleFactory.create(type, "32") as Article;
            if (art != null)
            {
                art.Designation = "Farbe";
                art.IsPurchase = true;
                art.Price = 0.75;
                art.Discount = 2700;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 2.1;
                art.DiliverDeviation = 0.5;
                art.update();
            }

            art = ArticleFactory.create(type, "33") as Article;
            if (art != null)
            {
                art.Designation = "Felge";
                art.IsPurchase = true;
                art.Price = 22.00;
                art.Discount = 900;
                art.OrderPriceNormal = 75.00;
                art.DeliverTime = 1.9;
                art.DiliverDeviation = 0.5;
                art.update();
            }

            art = ArticleFactory.create(type, "34") as Article;
            if (art != null)
            {
                art.Designation = "Speiche";
                art.IsPurchase = true;
                art.Price = 0.10;
                art.Discount = 22000;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.6;
                art.DiliverDeviation = 0.3;
                art.update();
            }

            art = ArticleFactory.create(type, "35") as Article;
            if (art != null)
            {
                art.Designation = "Nabe";
                art.IsPurchase = true;
                art.Price = 1.00;
                art.Discount = 3600;
                art.OrderPriceNormal = 75.00;
                art.DeliverTime = 2.2;
                art.DiliverDeviation = 0.4;
                art.update();
            }

            art = ArticleFactory.create(type, "36") as Article;
            if (art != null)
            {
                art.Designation = "Freilauf";
                art.IsPurchase = true;
                art.Price = 8;
                art.Discount = 900;
                art.OrderPriceNormal = 100.00;
                art.DeliverTime = 1.2;
                art.DiliverDeviation = 0.1;
                art.update();
            }

            art = ArticleFactory.create(type, "37") as Article;
            if (art != null)
            {
                art.Designation = "Gabel";
                art.IsPurchase = true;
                art.Price = 1.50;
                art.Discount = 900;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.5;
                art.DiliverDeviation = 0.3;
                art.update();
            }

            art = ArticleFactory.create(type, "38") as Article;
            if (art != null)
            {
                art.Designation = "Welle";
                art.IsPurchase = true;
                art.Price = 1.50;
                art.Discount = 300;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.7;
                art.DiliverDeviation = 0.4;
                art.update();
            }

            art = ArticleFactory.create(type, "39") as Article;
            if (art != null)
            {
                art.Designation = "Blech";
                art.IsPurchase = true;
                art.Price = 1.50;
                art.Discount = 1800;
                art.OrderPriceNormal = 75.00;
                art.DeliverTime = 1.5;
                art.DiliverDeviation = 0.3;
                art.update();
            }

            art = ArticleFactory.create(type, "40") as Article;
            if (art != null)
            {
                art.Designation = "Lenker";
                art.IsPurchase = true;
                art.Price = 2.50;
                art.Discount = 900;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.7;
                art.DiliverDeviation = 0.2;
                art.update();
            }

            art = ArticleFactory.create(type, "41") as Article;
            if (art != null)
            {
                art.Designation = "Mutter 3/4";
                art.IsPurchase = true;
                art.Price = 0.06;
                art.Discount = 900;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 0.9;
                art.DiliverDeviation = 0.2;
                art.update();
            }

            art = ArticleFactory.create(type, "42") as Article;
            if (art != null)
            {
                art.Designation = "Griff";
                art.IsPurchase = true;
                art.Price = .10;
                art.Discount = 1800;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.2;
                art.DiliverDeviation = 0.3;
                art.update();
            }

            art = ArticleFactory.create(type, "43") as Article;
            if (art != null)
            {
                art.Designation = "Sattel";
                art.IsPurchase = true;
                art.Price = 5.00;
                art.Discount = 2700;
                art.OrderPriceNormal = 75.00;
                art.DeliverTime = 2.0;
                art.DiliverDeviation = 0.5;
                art.update();
            }

            art = ArticleFactory.create(type, "44") as Article;
            if (art != null)
            {
                art.Designation = "Stange 1/2";
                art.IsPurchase = true;
                art.Price = 0.50;
                art.Discount = 900;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.0;
                art.DiliverDeviation = 0.2;
                art.update();
            }

            art = ArticleFactory.create(type, "45") as Article;
            if (art != null)
            {
                art.Designation = "Mutter 1/4";
                art.IsPurchase = true;
                art.Price = 0.06;
                art.Discount = 900;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.7;
                art.DiliverDeviation = 0.3;
                art.update();
            }

            art = ArticleFactory.create(type, "46") as Article;
            if (art != null)
            {
                art.Designation = "Schraube 1/4";
                art.IsPurchase = true;
                art.Price = 0.10;
                art.Discount = 900;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 0.9;
                art.DiliverDeviation = 0.3;
                art.update();
            }

            art = ArticleFactory.create(type, "47") as Article;
            if (art != null)
            {
                art.Designation = "Zahnkranz";
                art.IsPurchase = true;
                art.Price = 3.50;
                art.Discount = 900;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.1;
                art.DiliverDeviation = 0.1;
                art.update();
            }

            art = ArticleFactory.create(type, "48") as Article;
            if (art != null)
            {
                art.Designation = "Pedal";
                art.IsPurchase = true;
                art.Price = 1.50;
                art.Discount = 1800;
                art.OrderPriceNormal = 75.00;
                art.DeliverTime = 1.0;
                art.DiliverDeviation = 0.1;
                art.update();
            }

            art = ArticleFactory.create(type, "52") as Article;
            if (art != null)
            {
                art.Designation = "Felge";
                art.IsPurchase = true;
                art.Price = 22.00;
                art.Discount = 600;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.6;
                art.DiliverDeviation = 0.4;
                art.update();
            }

            art = ArticleFactory.create(type, "53") as Article;
            if (art != null)
            {
                art.Designation = "Speiche";
                art.IsPurchase = true;
                art.Price = 0.10;
                art.Discount = 22000;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.6;
                art.DiliverDeviation = 0.2;
                art.update();
            }

            art = ArticleFactory.create(type, "57") as Article;
            if (art != null)
            {
                art.Designation = "Felge";
                art.IsPurchase = true;
                art.Price = 22.00;
                art.Discount = 600;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.7;
                art.DiliverDeviation = 0.3;
                art.update();
            }

            art = ArticleFactory.create(type, "58") as Article;
            if (art != null)
            {
                art.Designation = "Speiche";
                art.IsPurchase = true;
                art.Price = 0.10;
                art.Discount = 22000;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 1.6;
                art.DiliverDeviation = 0.5;
                art.update();
            }

            art = ArticleFactory.create(type, "59") as Article;
            if (art != null)
            {
                art.Designation = "Schweißdraht";
                art.IsPurchase = true;
                art.Price = 0.15;
                art.Discount = 1800;
                art.OrderPriceNormal = 50.00;
                art.DeliverTime = 0.7;
                art.DiliverDeviation = 0.2;
                art.update();
            }



            //Produktionsteile
            
            //P1
            art = ArticleFactory.create(type, "18") as Article;
            art.Designation = "Rahmen";
            art.IsProduction = true;
            art.Price = 13.15;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("28");
            bomPos.amount = 3;
            bomPos.update();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("59");
            bomPos.amount = 2;
            bomPos.update();

            art = ArticleFactory.create(type, "13") as Article;
            art.Designation = "Schutzblech v.";
            art.IsProduction = true;
            art.Price = 12.40;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("39");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "7") as Article;
            art.Designation = "Vorderradgruppe";
            art.IsProduction = true;
            art.Price = 35.85;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("35");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("37");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("38");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("52");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("53");
            bomPos.amount = 36;
            bomPos.update();

            art = ArticleFactory.create(type, "49") as Article;
            art.Designation = "Vorderrad cpl.";
            art.IsProduction = true;
            art.Price = 64.64;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("25");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("7");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("13");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("18");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "10") as Article;
            art.Designation = "Schutzblech hinten";
            art.IsProduction = true;
            art.Price = 12.40;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("39");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "4") as Article;
            art.Designation = "Hinterradgruppe";
            art.IsProduction = true;
            art.Price = 40.85;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("35");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("36");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("52");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("53");
            bomPos.amount = 36;
            bomPos.update();

            art = ArticleFactory.create(type, "50") as Article;
            art.Designation = "Rahmen u. Räder";
            art.IsProduction = true;
            art.Price = 120.63;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("25");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("4");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("10");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("49");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "17") as Article;
            art.Designation = "Sattel";
            art.IsProduction = true;
            art.Price = 7.16;
            art.safetyStock = 300;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("43");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("44");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("45");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("46");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "16") as Article;
            art.Designation = "Lenker";
            art.IsProduction = true;
            art.Price = 7.02;
            art.safetyStock = 300;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("28");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("40");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("41");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("42");
            bomPos.amount = 2;
            bomPos.update();

            art = ArticleFactory.create(type, "51") as Article;
            art.Designation = "Fahrrad o. Pedal";
            art.IsProduction = true;
            art.Price = 137.47;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("27");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("16");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("17");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("50");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "26") as Article;
            art.Designation = "Pedal";
            art.IsProduction = true;
            art.Price = 10.50;
            art.safetyStock = 300;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("44");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("47");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("48");
            bomPos.amount = 2;
            bomPos.update();

            art = ArticleFactory.create(type, "1") as Article;
            art.Designation = "Kinderfahrrad";
            art.IsProduction = true;
            art.Price = 156.13;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("21");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("24");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("27");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("26");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("51");
            bomPos.amount = 1;
            bomPos.update();
            
            //P2
            art = ArticleFactory.create(type, "19") as Article;
            art.Designation = "Rahmen";
            art.IsProduction = true;
            art.Price = 14.35;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("28");
            bomPos.amount = 4;
            bomPos.update();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("59");
            bomPos.amount = 2;
            bomPos.update();

            art = ArticleFactory.create(type, "14") as Article;
            art.Designation = "Schutzblech v.";
            art.IsProduction = true;
            art.Price = 14.65;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("39");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "8") as Article;
            art.Designation = "Vorderradgruppe";
            art.IsProduction = true;
            art.Price = 35.85;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("35");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("37");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("38");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("57");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("58");
            bomPos.amount = 36;
            bomPos.update();

            art = ArticleFactory.create(type, "54") as Article;
            art.Designation = "Vorderrad";
            art.IsProduction = true;
            art.Price = 68.09;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("25");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("8");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("14");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("19");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "11") as Article;
            art.Designation = "Schutzblech h.";
            art.IsProduction = true;
            art.Price = 14.65;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("39");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "5") as Article;
            art.Designation = "Hinterradgruppe";
            art.IsProduction = true;
            art.Price = 39.85;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("35");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("36");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("57");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("58");
            bomPos.amount = 36;
            bomPos.update();

            art = ArticleFactory.create(type, "55") as Article;
            art.Designation = "Rahmen u. Räder";
            art.IsProduction = true;
            art.Price = 125.33;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("25");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("5");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("11");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("54");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "56") as Article;
            art.Designation = "Fahrrad ohne Pedal";
            art.IsProduction = true;
            art.Price = 142.67;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("27");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("16");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("17");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("55");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "2") as Article;
            art.Designation = "Damenfahrrad";
            art.IsProduction = true;
            art.Price = 163.33;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("22");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("24");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("27");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("26");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("56");
            bomPos.amount = 1;
            bomPos.update();

            //P3
            art = ArticleFactory.create(type, "20") as Article;
            art.Designation = "Rahmen";
            art.IsProduction = true;
            art.Price = 15.55;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("28");
            bomPos.amount = 5;
            bomPos.update();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("59");
            bomPos.amount = 2;
            bomPos.update();

            art = ArticleFactory.create(type, "15") as Article;
            art.Designation = "Schutzblech v.";
            art.IsProduction = true;
            art.Price = 14.65;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("39");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "9") as Article;
            art.Designation = "Vorderradgruppe";
            art.IsProduction = true;
            art.Price = 35.85;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("33");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("34");
            bomPos.amount = 36;
            bomPos.update();
            bomPos = bom.addPos("35");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("37");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("38");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "29") as Article;
            art.Designation = "Vorderrad mont.";
            art.IsProduction = true;
            art.Price = 69.29;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("25");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("9");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("15");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("20");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "12") as Article;
            art.Designation = "Schutzblech h.";
            art.IsProduction = true;
            art.Price = 14.65;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("32");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("39");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "6") as Article;
            art.Designation = "Hinterradgruppe";
            art.IsProduction = true;
            art.Price = 40.85;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("33");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("34");
            bomPos.amount = 36;
            bomPos.update();
            bomPos = bom.addPos("35");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("36");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "30") as Article;
            art.Designation = "Rahmen o. Räder";
            art.IsProduction = true;
            art.Price = 127.53;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("25");
            bomPos.amount = 2;
            bomPos.update();
            bomPos = bom.addPos("6");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("12");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("29");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "31") as Article;
            art.Designation = "Rahmen o. Ped.";
            art.IsProduction = true;
            art.Price = 144.42;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("24");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("27");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("16");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("17");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("30");
            bomPos.amount = 1;
            bomPos.update();

            art = ArticleFactory.create(type, "3") as Article;
            art.Designation = "Herrenfahrrad";
            art.IsProduction = true;
            art.Price = 165.08;
            art.safetyStock = 100;
            art.update();
            bom = art.createBom();
            bomPos = bom.addPos("23");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("24");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("27");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("26");
            bomPos.amount = 1;
            bomPos.update();
            bomPos = bom.addPos("31");
            bomPos.amount = 1;
            bomPos.update();

            foreach (Article a in ArticleFactory.getAllArticle())
            {
                if (a.IsProduction)
                {
                    a.use = a.getUse();
                    a.update();
                }
            }
        }
    }
}
