using CVARC.V2;
using HoMM.Robot;
using HoMM.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Units.HexagonalMovement
{
    class UpdateLocationTrigger : OneTimeTrigger
    {
        public UpdateLocationTrigger(double turnDuration, IHommRobot actor, Location newLocation)
            : base(actor.World.Clocks.CurrentTime + turnDuration / 2, () =>
                DoUpdateLocation(actor.World.Clocks.CurrentTime + turnDuration, actor, newLocation))
        { }

        private static void DoUpdateLocation(double turnEnd, IHommRobot actor, Location newLocation)
        {
            var controlTriggerWakekup = turnEnd;

            foreach (var otherPlayer in actor.World.Players)
                if (otherPlayer.Location == newLocation)
                {
                    controlTriggerWakekup += HommRules.Current.CombatDuration;
                    Combat.ResolveBattle(actor.Player, otherPlayer);
                }

            actor.World.Round.Update(actor.Player, newLocation);

            actor.World.Clocks.AddTrigger(new OneTimeTrigger(controlTriggerWakekup,
                () => actor.ControlTrigger.Reschedule(controlTriggerWakekup)));
        } 
    }
}
