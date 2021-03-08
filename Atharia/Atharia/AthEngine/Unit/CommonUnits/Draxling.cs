using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Draxling {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectBaseMovementTimeUCCreate(0, 50).AsIUnitComponent());
      components.Add(root.EffectBaseCombatTimeUCCreate(0, 50).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(0, 60).AsIUnitComponent());
      return root.EffectUnitCreate(
        NullIUnitEvent.Null,
              0,
              new Location(0, 0, 0),
              "draxling",
              0,
              5, 5,
              components,
              false);
    }
  }
}
