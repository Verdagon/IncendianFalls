using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDirtRoadLevelControllerEffect : IEffect {
  int id { get; }
  void visitIDirtRoadLevelControllerEffect(IDirtRoadLevelControllerEffectVisitor visitor);
}
       
}
