using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Data;
using Planning_Tool.Core;

namespace Planning_Tool.Masterdata
{
    class ArticleFactory : PlanningObjectFactory
    {
        public static List<Article> getAllArticle()
        {
            List<Article> artList = new List<Article>();
            List<PlanningObject> obj = null;
            obj = PlanningObjectFactory.searchAll(typeof(Article));
            foreach(PlanningObject o in obj)
            {
                artList.Add(o as Article);
            }

            return artList;
        }

        /// <summary>
        /// Gibt alle Stücklistenpositionen zu diesem Artikel zurück
        /// </summary>
        /// <param name="bom"></param>
        /// <param name="bompos"></param>
        public static void getAllBomPos(string bom,List<BOMpos> bompos)
        {
            BOMpos pos = null;
            foreach(PlanningPosObject o in BOMposFactory.search(typeof(BOMpos),bom))
            {
                pos = o as BOMpos;
                if(pos.isModule()){
                    getAllBomPos(pos.bompos,bompos);
                }
                bompos.Add(pos);
            }
        }

        /// <summary>
        /// Gibt alle Stücklistenpositionen zu diesem Artikel zurück
        /// </summary>
        /// <param name="bom"></param>
        /// <param name="boms"></param>
        public static void getAllModule(string bom, List<BOMpos> bompos)
        {
            BOMpos pos = null;
            foreach (PlanningPosObject o in BOMposFactory.search(typeof(BOMpos), bom))
            {
                pos = o as BOMpos;
                if (pos.isModule())
                {
                    getAllModule(pos.bompos, bompos);
                    bompos.Add(pos);
                }
            }
        }

        public static void getModule(string bom, List<BOMpos> bompos)
        {
            BOMpos pos = null;
        }
    }
}
