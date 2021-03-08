using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICaveLevelControllerEffect : IEffect {
  int id { get; }
  void visitICaveLevelControllerEffect(ICaveLevelControllerEffectVisitor visitor);
}
       
}
