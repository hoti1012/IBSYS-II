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
    }
}
