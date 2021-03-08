using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGauntletLevelControllerEffect : IEffect {
  int id { get; }
  void visitIGauntletLevelControllerEffect(IGauntletLevelControllerEffectVisitor visitor);
}
       
}
