using System.Collections.Generic;

namespace HoMM
{
    public class Garrison : CapturableObject
    {
        public override bool IsPassable => true;

        public Dictionary<UnitType, int> Guards { get; private set; }
        public Garrison(Dictionary<UnitType, int> guards, Location location) : base(location)
        {
            Guards = guards;
        }

        public override void InteractWithPlayer(Player p)
        {
            if (p != Owner)
            {
                var tempPlayer = new Player("Garrison", null, 0, 0);
                foreach (var kvp in Guards)
                    tempPlayer.AddUnits(kvp.Key, kvp.Value);
                Combat.ResolveBattle(p, tempPlayer);
                if (tempPlayer.HasNoArmy)
                    Owner = p;
                else
                    foreach (var key in Guards.Keys)
                        Guards[key] = tempPlayer.Army[key];
            }
        }
    }
}
