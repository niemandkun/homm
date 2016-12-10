using CVARC.V2;
using HoMM.Engine;
using HoMM.Rules;
using HoMM.Sensors;
using HoMM.Units.ArmyInterface;
using HoMM.Units.HexagonalMovement;
using HoMM.World;
using System.Collections.Generic;
using System.Linq;

namespace HoMM.Robot
{
    class HeroRobot : Robot<HommWorld, HommSensorData, HommCommand, HommRules>,
        IHommRobot
    {
        public override IEnumerable<IUnit> Units { get; }
        
        public Player Player { get; }
        public IHommEngine HommEngine { get; }
        public Map Map => World.Round.Map;

        public HeroRobot()
        {
            Player = World.Players.Where(p => p.Name == ControllerId).Single();

            Units = new List<IUnit>
            {
                new HexMovUnit(this),
                new ArmyInterfaceUnit(this),
            };
        }
    }
}
