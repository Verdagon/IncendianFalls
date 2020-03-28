using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Ravashrike {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectSorcerousUCCreate(100, 100).AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(11, 100).AsIUnitComponent());
      components.Add(root.EffectBaseCombatTimeUCCreate(0, 40).AsIUnitComponent());
      components.Add(root.EffectBaseMovementTimeUCCreate(0, 40).AsIUnitComponent());
      return root.EffectUnitCreate(
          NullIUnitEvent.Null,
          true,
          0,
          new Location(0, 0, 0),
          "Ravashrike",
          0,
          600, 600,
          components,
          false);
    }
  }
}
