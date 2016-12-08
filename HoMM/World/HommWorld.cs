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

        public override void CreateWorld()
        {
            Random = new Random(WorldState.Seed);
            HommEngine = GetEngine<IHommEngine>();
            CommonEngine = GetEngine<ICommonEngine>();
            
            var map = MapHelper.CreateMap(Random);
            var players = new Player[] {
                new Player(TwoPlayersId.Left, map),
                new Player(TwoPlayersId.Right, map),
            };

            Round = new Round(map, players);
        }
    }
}
