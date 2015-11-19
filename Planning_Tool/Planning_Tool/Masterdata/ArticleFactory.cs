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
        public static Article search(string article)
        {
            DatabaseManager manager = null;
            string where = "WHERE " + typeof(Article).Name + " = " + article;
            List<Object> objects;
            Article articleObj = null;

            try
            {
                manager = new DatabaseManager();
                objects = manager.get(typeof(Article),where);

                if (objects == null)
                {
                    return null;
                }

                if (objects.Count > 1)
                {
                    //TODO: Exception werfen
                }

                articleObj = objects[0] as Article;

            }
            finally
            {
                if (manager != null && manager.Open)
                {
                    manager.release();
                }
            }

            return articleObj;
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
                objects = manager.get(typeof(Article), where);
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
                objects = manager.get(typeof(Article), where);
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
        /// Speichert sämtliche änderungen in die Datenbank
        /// </summary>
        /// <param name="article">ArtikelObject</param>
        public static void update(Article article)
        {
            DatabaseManager manager = new DatabaseManager();
            try
            {
                manager.update(article);
            }
            finally
            {
                manager.release();
            }
        }
    }
}
