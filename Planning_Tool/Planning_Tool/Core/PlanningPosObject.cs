using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Core
{
    public abstract class PlanningPosObject
    {
        /// <summary>
        /// Speichert die änderungen am Object in der Datenbank
        /// </summary>
        public void update()
        {
            PlanningPosObjectFactory.update(this);
        }

        /// <summary>
        /// Löscht das Object aus der Datenbankt
        /// </summary>
        public void delete()
        {
            PlanningPosObjectFactory.delete(this);
        }

        /// <summary>
        /// Gibt das Kopfobject zu welcher das Positionsobject gehört zurück
        /// </summary>
        /// <returns>Kopfobject</returns>
        public PlanningObject getHead(Type headType)
        {
            return PlanningPosObjectFactory.getHead(this,headType);
        }

        /// <summary>
        /// Fügt ein Object in die Datenbank hinzu
        /// </summary>
        public void create()
        {
            PlanningPosObjectFactory.create(this);
        }

    }
}
