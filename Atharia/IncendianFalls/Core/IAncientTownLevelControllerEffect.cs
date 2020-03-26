using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IAncientTownLevelControllerEffect : IEffect {
  int id { get; }
  void visitIAncientTownLevelControllerEffect(IAncientTownLevelControllerEffectVisitor visitor);
}
       
}
