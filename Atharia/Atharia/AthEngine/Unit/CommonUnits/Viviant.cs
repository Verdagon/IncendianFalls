using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Viviant {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectEvolvifyAICapabilityUCCreate().AsIUnitComponent());
      // components.Add(root.EffectBaseSightRangeUCCreate(0, 50).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(0, 70).AsIUnitComponent());
      components.Add(root.EffectBequeathUCCreate("HealthPotion").AsIUnitComponent());
      components.Add(root.EffectDeathTriggerUCCreate("viviantDied").AsIUnitComponent());
      return root.EffectUnitCreate(
          NullIUnitEvent.Null,
          0,
          new Location(0, 0, 0),
          "Viviant",
          0,
          34, 34,
          components,
          false);
    }
  }
}
