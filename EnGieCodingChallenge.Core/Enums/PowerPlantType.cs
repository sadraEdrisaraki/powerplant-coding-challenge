using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnGieCodingChallenge.Core.Enums
{
    public class PowerPlantType : Enumeration
    {
        public static readonly PowerPlantType GasFired
        = new PowerPlantType(0, "gasfired");
        public static readonly PowerPlantType TurboJet
            = new PowerPlantType(1, "turbojet");
        public static readonly PowerPlantType WindTurbine
            = new PowerPlantType(2, "windturbine");

        public PowerPlantType() { }
        public PowerPlantType(int value, string displayName) : base(value, displayName) { }

    }
}
