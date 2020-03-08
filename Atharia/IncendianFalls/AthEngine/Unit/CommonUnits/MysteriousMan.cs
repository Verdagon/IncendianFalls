using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncendianFalls {
  static class MysteriousMan {
    public static Unit Make(Root root) {
      var components = IUnitComponentMutBunch.New(root);
      components.Add(root.EffectBaseDefenseUCCreate(0, 0).AsIUnitComponent());
      return root.EffectUnitCreate(
          root.EffectIUnitEventMutListCreate(),
          true,
          0,
          new Location(0, 0, 0),
          "MysteriousMan",
          0,
          1000, 1000,
          components,
          false);
    }
  }
}
