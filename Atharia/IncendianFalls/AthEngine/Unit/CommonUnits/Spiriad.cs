using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Spiriad {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(130, 100).AsIUnitComponent());
      return
          root.EffectUnitCreate(
              NullIUnitEvent.Null,
              true,
              0,
              new Location(0, 0, 0),
              "spiriad",
              0,
              50, 50,
              components,
              false);
    }
  }
}
