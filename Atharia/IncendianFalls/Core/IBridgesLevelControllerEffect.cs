using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBridgesLevelControllerEffect : IEffect {
  int id { get; }
  void visitIBridgesLevelControllerEffect(IBridgesLevelControllerEffectVisitor visitor);
}
       
}
