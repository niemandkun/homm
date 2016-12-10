using System;
using System.Linq;
using CVARC.V2;
using HoMM.Engine;
using HoMM.Rules;

namespace HoMM.World
{
    sealed class HommWorld : World<HommWorldState>
    {
        public HommEngine HommEngine { get; private set; }
        public ICommonEngine CommonEngine { get; private set; }
        public Round Round { get; private set; }
        public Random Random { get; private set; }

        public Player[] Players { get; private set; }

        private string[] players;

        public HommWorld(params string[] players) : base()
        {
            this.players = players;
        }

        public override void CreateWorld()
        {
            Random = new Random(WorldState.Seed);
            CommonEngine = GetEngine<ICommonEngine>();
            HommEngine = GetEngine<HommEngine>();

            var map = MapHelper.CreateMap(Random);
            Players = players.Select(pid => GetPlayer(pid, map)).ToArray();            
            Round = new Round(map, Players);

            MapUnityConnecter.Connect(Round, HommEngine);

            Clocks.AddTrigger(new TimerTrigger(_ => Round.DailyTick(), HommRules.Current.DailyTickInterval));
        }

        private Player GetPlayer(string controllerId, Map map)
        {
            var player = new Player(controllerId, map);
            player.Location = controllerId == TwoPlayersId.Left
                ? Location.Zero
                : new Location(map.Size.Y - 1, map.Size.X - 1);

            return player;
        }
    }
}
