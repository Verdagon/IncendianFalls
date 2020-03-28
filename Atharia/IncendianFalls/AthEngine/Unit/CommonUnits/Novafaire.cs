using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Novafaire {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(3, 100).AsIUnitComponent());
      components.Add(root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      return
          root.EffectUnitCreate(
              NullIUnitEvent.Null,
              true,
              0,
              new Location(0, 0, 0),
              "novafaire",
              0,
              27, 27,
              components,
              false);
    }
  }
}
