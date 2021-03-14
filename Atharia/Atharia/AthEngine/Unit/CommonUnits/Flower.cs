using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Flower {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(0, 80).AsIUnitComponent());
      return root.EffectUnitCreate(
        NullIUnitEvent.Null,
              0,
              new Location(0, 0, 0),
              "Flower",
              0,
              4, 4,
              components,
              false);
    }
  }
}
