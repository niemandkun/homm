using CVARC.V2;
using HoMM.Robot;

namespace HoMM.Units.HexagonalMovement
{
    class HexMovUnit : IUnit
    {
        private IHommRobot actor;

        public HexMovUnit(IHommRobot actor)
        {
            this.actor = actor;
        }

        public UnitResponse ProcessCommand(object command)
        {
            var movement = Compatibility.Check<IHexMovCommand>(this, command).Movement;
            if (movement == null) return UnitResponse.Denied();

            var movementResult = movement.TryMoveHero(actor.World.HommEngine, actor.Player, actor.Map);
            var newLocation = movementResult.Item1;
            var movementDuration = movementResult.Item2;

            actor.World.Clocks.AddTrigger(new OneTimeTrigger(movementDuration / 2, 
                () => actor.Player.Location = newLocation));

            return UnitResponse.Accepted(movementDuration);
        }
    }
}
