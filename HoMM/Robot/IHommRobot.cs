﻿using CVARC.V2;
using HoMM.World;

namespace HoMM.Robot
{
    interface IHommRobot : IActor
    {
        new HommWorld World { get; }
        Map Map { get; }
        Player Player { get; }
    }
}