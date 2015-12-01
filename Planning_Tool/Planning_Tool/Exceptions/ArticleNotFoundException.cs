using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Exceptions
{
    class ArticleNotFoundException : Exception
    {

        public ArticleNotFoundException(string artNr)
            : base("Der Artikel mit der Artikelnummer: \"" + artNr + "\" konnte nicht gefunden werden"){ }
    }
}
