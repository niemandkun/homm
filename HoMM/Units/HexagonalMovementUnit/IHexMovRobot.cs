using CVARC.V2;
using HoMM.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Units.HexagonalMovementUnit
{
    interface IHexMovRobot : IActor
    {
        new HommWorld World { get; }
        Location Location { get; set; }
        double VelocityModifier { get; }
    }
}
