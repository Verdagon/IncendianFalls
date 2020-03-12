using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class MantisBombardier {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectBaseOffenseUCCreate(25, 100).AsIUnitComponent());
      components.Add(root.EffectKamikazeAICapabilityUCCreate(
        root.EffectKamikazeTargetTTCStrongByLocationMutMapCreate(),
        new Location(0, 0, 0)).AsIUnitComponent());
      return
          root.EffectUnitCreate(
              root.EffectIUnitEventMutListCreate(),
              true,
              0,
              new Location(0, 0, 0),
              "MantisBombardier",
              0,
              70, 70,
              components,
              false);
    }
  }
}
