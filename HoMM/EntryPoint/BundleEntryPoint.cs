using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;
using UnityCommons;
using HoMM.Engine;

namespace HoMM.EntryPoint
{
    public class BundleEntryPoint : IBundleEntryPoint
    {
        public IEnumerable<Competitions> GetLevels()
        {
            yield return new Competitions("level1", new HommLogicPartHelper(2), () => new UKeyboard(), CreateEngines);
        }

        private List<IEngine> CreateEngines()
        {
            var commonEngine = new CommonEngine();
            var hommEngine = new HommEngine(commonEngine);
            return new List<IEngine> { commonEngine, hommEngine };
        }
    }
}
