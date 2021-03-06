﻿using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class Chronomancer {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(root.EffectSorcerousUCCreate(100, 100).AsIUnitComponent());
      return root.EffectUnitCreate(
        NullIUnitEvent.Null,
          0,
          new Location(0, 0, 0),
          "Chronomancer",
          0,
          30, 30,
          components,
          true);
    }
  }
}
