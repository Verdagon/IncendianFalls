using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Irkling {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(0, 80).AsIUnitComponent());
      return root.EffectUnitCreate(
        NullIUnitEvent.Null,
              true,
              0,
              new Location(0, 0, 0),
              "Irkling",
              0,
              4, 4,
              components,
              false);
    }
  }
}
