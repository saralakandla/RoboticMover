using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Core_3._0.Model
{
    public class CloseRobot
    {
        public int robotId { get; set; }
        public double distanceToGoal { get; set; }
        public int batteryLevel { get; set; }
    }
}
