using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Baug {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      return root.EffectUnitCreate(
          root.EffectIUnitEventMutListCreate(),
          true,
          0,
          new Location(0, 0, 0),
          "Baug",
          0,
          24, 24,
          components,
          false);
    }
  }
}
