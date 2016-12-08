using CVARC.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Units.HexagonalMovementUnit
{
    class HexMovUnit : IUnit
    {
        private IHexMovRobot actor;
        private IHexMovRules rules;

        public HexMovUnit(IHexMovRobot actor)
        {
            this.actor = actor;
            this.rules = Compatibility.Check<IHexMovRules>(this, actor.Rules);
        }

        public UnitResponse ProcessCommand(object _command)
        {
            var movement = Compatibility.Check<IHexMovCommand>(this, _command).Movement;
            if (movement == null) return UnitResponse.Denied();

            var newLocation = movement.Turn(actor.Location);
            var commandDuration = rules.MovementDuration * actor.VelocityModifier;

            if (!newLocation.IsInside(actor.World.Round.Map.Size) ||
                (!actor.World.Round.Map[newLocation].tileObject?.IsPassable ?? false))
                return UnitResponse.Accepted(commandDuration);

            // actor.World.HommEngine ?? TODO: start animation

            actor.World.Clocks.AddTrigger(new OneTimeTrigger(commandDuration / 2, 
                () => actor.Location = movement.Turn(actor.Location)));

            return UnitResponse.Accepted(commandDuration);
        }
    }
}
