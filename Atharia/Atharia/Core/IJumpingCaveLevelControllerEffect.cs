using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IJumpingCaveLevelControllerEffect : IEffect {
  int id { get; }
  void visitIJumpingCaveLevelControllerEffect(IJumpingCaveLevelControllerEffectVisitor visitor);
}
       
}
