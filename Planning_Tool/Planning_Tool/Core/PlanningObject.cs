using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Core
{
    public abstract class PlanningObject
    {
        /// <summary>
        /// Speichert die änderungen am Object in der Datenbank
        /// </summary>
        public void update()
        {
            PlanningObjectFactory.update(this);
        }

        /// <summary>
        /// Löscht das Object aus der Datenbankt
        /// </summary>
        public void delete()
        {
            PlanningObjectFactory.delete(this);
        }
    }
}
