using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Data;

namespace Planning_Tool.Masterdata
{
    class ArticleFactory
    {
        /// <summary>
        /// Erstellt einen neuen Artikel
        /// </summary>
        /// <returns>Gibt den neu erzeugten Artikel zurück</returns>
        public static Article create()
        {
            return new Article();
        }

        /// <summary>
        /// Sucht einen Artikel anhand einer Artikelnummer
        /// </summary>
        /// <param name="articleNumber">Artikelnummer des Gesuchten Artikels</param>
        /// <returns>Den gesuchten Artikel</returns>
        public static Article search(string articleNumber)
        {
            DatabaseManager manager = null;
            string where = "WHERE articleNumber = " + articleNumber;
            List<Object> objects;
            Article article = null;

            try
            {
                manager = new DatabaseManager();
                objects = manager.load(typeof(Article),where);

                if (objects == null)
                {
                    return null;
                }

                if (objects.Count > 1)
                {
                    //TODO: Exception werfen
                }

                article = objects[0] as Article;

            }
            finally
            {
                if (manager != null && manager.Open)
                {
                    manager.release();
                }
            }

            return article;
        }

        /// <summary>
        /// Liefert sämtliche einkaufs Artikel zurück
        /// </summary>
        /// <returns></returns>
        public static List<Article> serachPurchase()
        {
            DatabaseManager manager = null;
            string where = "WHERE isPurchase = 'true'";
            List<Object> objects;
            List<Article> articles;

            try
            {
                manager = new DatabaseManager();
                objects = manager.load(typeof(Article), where);
                articles = new List<Article>();

                if (objects == null)
                {
                    return null;
                }

                foreach (Object o in objects)
                {
                    articles.Add(o as Article);
                }
            }
            finally
            {
                if (manager != null && manager.Open)
                {
                    manager.release();
                }
            }

            return articles;
        }

        /// <summary>
        /// Liefert sämtlichen selbst produzierten Artikel zurück
        /// </summary>
        /// <returns>List<Article> mit allen Artikeln</returns>
        public static List<Article> serachProduction()
        {
            DatabaseManager manager = null;
            string where = "WHERE isProduction = 'true'";
            List<Object> objects;
            List<Article> articles;

            try
            {
                manager = new DatabaseManager();
                objects = manager.load(typeof(Article), where);
                articles = new List<Article>();

                if (objects == null)
                {
                    return null;
                }

                foreach (Object o in objects)
                {
                    articles.Add(o as Article);
                }
            }
            finally
            {
                if (manager != null && manager.Open)
                {
                    manager.release();
                }
            }

            return articles;
        }
    }
}
