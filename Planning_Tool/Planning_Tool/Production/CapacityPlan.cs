using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Core;

namespace Planning_Tool.Production
{
    class CapacityPlan : PlanningObject
    {
        /// <summary>
        /// Beinhaltet den Arbeitsplatz
        /// </summary>
        private string _capacityPlan;

        public string capacityPlan
        {
            get { return _capacityPlan; }
            set { _capacityPlan = value; }
        }

        private int _neededTime;

        public int neededTime
        {
            get { return _neededTime; }
            set { _neededTime = value; }
        }

        private int _neededOldTime;

        public int neededOldTime
        {
            get { return _neededOldTime; }
            set { _neededOldTime = value; }
        }

        private int _overTime;

        public int overTime
        {
            get { return _overTime; }
            set { _overTime = value; }
        }

        private int _shift;

        public int shift
        {
            get { return _shift; }
            set { _shift = value; }
        }

        private int _makeReadyTime;

        public int makeReadyTime
        {
            get { return _makeReadyTime; }
            set { _makeReadyTime = value; }
        }

        private int _neededCompleteTime;

        public int neededCompleteTime
        {
            get { return _neededCompleteTime; }
            set { _neededCompleteTime = value; }
        }

        /// <summary>
        /// Fügt eine neue Position hinzu
        /// </summary>
        /// <param name="pos">nummer des Artikels</param>
        /// <param name="neededTime">benötigte Zeit</param>
        public void addPos(string pos,int neededTime,int makeReady)
        {
            CapacityPlanPos capaPos = CapacityPlanPosFactory.create(typeof(CapacityPlanPos), this._capacityPlan, pos) as CapacityPlanPos;
            if (capaPos == null)
            {
                capaPos = CapacityPlanPosFactory.search(typeof(CapacityPlanPos), this._capacityPlan, pos) as CapacityPlanPos;
            }
            capaPos.neededTime += neededTime;
            capaPos.neededMakeReady += makeReady;
            capaPos.update();
            this.neededTime += neededTime;
            this.makeReadyTime += makeReady;
        }

        internal void finalizeCapaPlan()
        {
            _neededCompleteTime = Convert.ToInt32(_neededTime + _neededOldTime + (_makeReadyTime * 1.2));
            setShift();
            setOvertime();
        }

        private void setOvertime()
        {
            if (_shift == 3)
            {
                _overTime = 0;
                return;
            }

            _overTime = (_neededCompleteTime - (_shift * 2400)) / 5;
            if (_overTime < 10)
            {
                _overTime = 0;
            }
        }

        private void setShift()
        {
            if (_neededCompleteTime <= 3600)
            {
                _shift = 1;
                return;
            }
            if (_neededCompleteTime <= 6000)
            {
                _shift = 2;
                return;
            }
            if (_neededCompleteTime > 6000)
            {
                _shift = 3;
                return;
            }
        }
    }
}
