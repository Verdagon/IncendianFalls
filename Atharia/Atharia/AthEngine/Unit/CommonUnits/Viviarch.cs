using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Viviarch {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectEvolvifyAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(0, 200).AsIUnitComponent());
      components.Add(root.EffectDeathTriggerUCCreate("viviarchDied").AsIUnitComponent());
      return root.EffectUnitCreate(
          NullIUnitEvent.Null,
          0,
          new Location(0, 0, 0),
          "Greater Viviarch",
          0,
          200, 200,
          components,
          false);
    }
  }
}
