using Planning_Tool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    class BOMFactory
    {
        /// <summary>
        /// Sucht nach einem Stücklistenkopf
        /// </summary>
        /// <param name="bom"></param>
        /// <returns>Das gefundene Stücklisten Kopfobejct</returns>
        public static BOM serch(string bom)
        {
            DatabaseManager manager = null;
            List<Object> list = null;
            string where;
            BOM bomObj = null;

            manager = new DatabaseManager();
            try
            {
                where = "WHERE " + typeof(BOM).Name + " = " + bom;
                list = manager.select(typeof(BOM), where);
                if (list != null && list.Count > 0)
                {
                    bomObj = list[0] as BOM;
                }
            }
            finally
            {
                manager.release();
            }

            return bomObj;
        }

        /// <summary>
        /// Erzeugt einen Stücklistenkopf
        /// </summary>
        /// <param name="article">Nummer des Artikles zu dem diese Stückliste gehört</param>
        /// <returns>Stücklistenkopf Object</returns>
        public static BOM create(string article)
        {
            BOM bomObj = null;
            DatabaseManager manager;
            Article articleObj;

            manager = new DatabaseManager();
            try
            {
                if(article != null)
                {
                    articleObj = ArticleFactory.search(article);
                    bomObj = new BOM();
                    bomObj.bom = article;
                    bomObj.designation = articleObj.Designation;
                    manager.insert(bomObj);
                }
            }
            finally
            {
                manager.release();
            }
            return bomObj;
        }
    }
}
