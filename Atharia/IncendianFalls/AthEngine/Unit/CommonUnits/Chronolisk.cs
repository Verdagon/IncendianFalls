using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Chronolisk {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectTemporaryCloneAICapabilityUCCreate("Chronolisk", 3).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(9, 100).AsIUnitComponent());
      return
          root.EffectUnitCreate(
        NullIUnitEvent.Null,
              true,
              0,
              new Location(0, 0, 0),
              "Chronolisk",
              0,
              40, 40,
              components,
              false);
    }
  }
}
