using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;
using HoMM.Generators;
using HoMM.Engine;

namespace HoMM.World
{
    sealed class HommWorld : World<HommWorldState>
    {
        public IHommEngine HommEngine { get; private set; }
        public ICommonEngine CommonEngine { get; private set; }
        public Round Round { get; private set; }
        public Random Random { get; private set; }

        public Player[] Players { get; private set; }

        public override void CreateWorld()
        {
            Random = new Random(WorldState.Seed);

            var map = MapHelper.CreateMap(Random);

            Players = new Player[] {
                new Player(TwoPlayersId.Left, map),
                new Player(TwoPlayersId.Right, map),
            };

            Round = new Round(map, Players);

            CommonEngine = GetEngine<ICommonEngine>();
            HommEngine hommEngine = GetEngine<HommEngine>();
            HommEngine = hommEngine;

            MapUnityConnecter.Connect(Round, hommEngine);
        }
    }
}
