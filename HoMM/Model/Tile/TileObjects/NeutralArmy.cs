namespace HoMM
{
    public class NeutralArmy : TileObject
    {
        public readonly Unit Unit;
        public CapturableObject GuardedObject { get; private set; }
        public int Quantity { get; private set; }
        
        public override bool IsPassable => true;

        public NeutralArmy(Unit unit, int quantity, Location location) : base(location)
        {
            Unit = unit;
            Quantity = quantity;
        }

        public void GuardObject(CapturableObject obj)
        {
            GuardedObject = obj;
        }

        public override void InteractWithPlayer(Player p)
        {
            var tempPlayer = new Player("Neutral army", null, 0, 0);
            tempPlayer.AddUnits(Unit.UnitType, Quantity);
            Combat.ResolveBattle(p, tempPlayer);
            if (tempPlayer.HasNoArmy)
                OnRemove();
            else
                Quantity = tempPlayer.Army[Unit.UnitType];
        }
    }
}
