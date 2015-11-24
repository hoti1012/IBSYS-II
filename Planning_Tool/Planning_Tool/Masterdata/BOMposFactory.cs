using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Data;
using Planning_Tool.Exceptions;

namespace Planning_Tool.Masterdata
{
    class BOMposFactory
    {
        /// <summary>
        /// Erzeugt eine Stücklistenposition
        /// </summary>
        /// <param name="bom"></param>
        /// <returns>Die erzeugte Stücklistenposition</returns>
        public static BOMpos create(string bom, string article)
        {
            DatabaseManager manager;
            BOMpos bomPosObj = null;
            BOM bomObj = null;
            Article articleObj = null;

            manager = new DatabaseManager();
            try
            {
                if (bom != null && article != null)
                {
                    bomObj = BOMFactory.serch(bom);
                    articleObj = ArticleFactory.search(article);
                    if (bomObj == null)
                    {
                        throw new NotFoundException(bom);
                    }
                    if (articleObj == null)
                    {
                        throw new NotFoundException(article);
                    }

                    bomPosObj = new BOMpos();
                    bomPosObj.bom = bom;
                    bomPosObj.bompos = article;
                    bomPosObj.designation = articleObj.Designation;
                    manager.insert(bomPosObj);
                }
            }
            finally
            {
                manager.release();
            }
            return bomPosObj;
        }

        /// <summary>
        /// Sucht eine Stücklistenposition
        /// </summary>
        /// <param name="bom">Stücklistennummer</param>
        /// <param name="bompos">Stücklistenposition</param>
        /// <returns>Stücklistenposion</returns>
        public static BOMpos search(string bom, string bompos)
        {
            List<Object> tmp;
            DatabaseManager manager;
            BOMpos res = null;
            string where = null;

            manager = new DatabaseManager();
            try
            {
                where  = "WHERE " + typeof(BOM).Name + " = " + bom;
                where += " AND " + typeof(BOMpos).Name + " = " + bompos;
                tmp = manager.select(typeof(BOMpos),where);
                if (tmp != null && tmp.Count > 0)
                {
                    res = tmp[0] as BOMpos;
                }
                else
                {
                    throw new NotFoundException(bom + "." + bompos);
                }
            }
            finally
            {
                manager.release();
            }

            return res;
        }

        /// <summary>
        /// Speichert die änderungen des Objects in die Datenbank
        /// </summary>
        /// <param name="BOMpos"></param>
        public static void Update(BOMpos BOMpos)
        {
            DatabaseManager manager = new DatabaseManager();
            try
            {
                manager.update(BOMpos);
            }
            finally
            {
                manager.release();
            }
        }

    }
}
