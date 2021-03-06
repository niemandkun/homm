﻿using CVARC.V2;
using HoMM.Engine;
using HoMM.Rules;
using HoMM.Sensors;
using HoMM.Units.ArmyInterface;
using HoMM.Units.HexagonalMovement;
using HoMM.World;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HoMM.Robot
{
    class HommRobot : Robot<HommWorld, HommSensorData, HommCommand, HommRules>,
        IHommRobot
    {
        public override IEnumerable<IUnit> Units { get; }
        
        public Player Player { get; }
        public IHommEngine HommEngine { get; }
        public Map Map => World.Round.Map;

        public LocationTrigger LocationTrigger { get; set; }

        public HommRobot()
        {
            Player = World.Players.Where(p => p.Name == ControllerId).Single();

            Units = new List<IUnit>
            {
                new HexMovUnit(this),
                new ArmyInterfaceUnit(this),
            };
        }

        public void Die()
        {
            World.CommonEngine.DeleteObject(ControllerId);
            var respawnTime = World.Clocks.CurrentTime + HommRules.Current.RespawnInterval;
            
            ControlTrigger.ScheduledTime = respawnTime + 1;
            Player.Location = World.GetRespawnLocation(ControllerId);

            World.Clocks.AddTrigger(new OneTimeTrigger(respawnTime, () =>
                World.HommEngine.CreateObject(ControllerId, MapObject.Hero, Player.Location.X, Player.Location.Y)));
        }
    }
}
