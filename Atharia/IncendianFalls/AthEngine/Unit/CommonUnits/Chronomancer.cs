using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Chronomancer {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectSorcerousUCCreate(100, 100).AsIUnitComponent());
      components.Add(root.EffectBlastRodCreate().AsIUnitComponent());
      return root.EffectUnitCreate(
          root.EffectIUnitEventMutListCreate(),
          true,
          0,
          new Location(0, 0, 0),
          "Chronomancer",
          0,
          90, 90,
          components,
          true);
    }
  }
}
