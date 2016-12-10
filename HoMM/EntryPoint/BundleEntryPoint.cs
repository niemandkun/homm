using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;
using UnityCommons;
using HoMM.Engine;

namespace HoMM.EntryPoint
{
    class BundleEntryPoint : IBundleEntryPoint
    {
        public IEnumerable<Competitions> GetLevels()
        {
            return new Competitions[]
            {
                new Competitions("level1", new HommLogicPartHelper(2), () => new UKeyboard(), () =>
                {
                    var commonEngine = new CommonEngine();
                    return new List<IEngine> { new CommonEngine(), new HommEngine(commonEngine) };
                })
            };
        }
    }
}
