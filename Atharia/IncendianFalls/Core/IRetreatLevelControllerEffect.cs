using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRetreatLevelControllerEffect : IEffect {
  int id { get; }
  void visitIRetreatLevelControllerEffect(IRetreatLevelControllerEffectVisitor visitor);
}
       
}
