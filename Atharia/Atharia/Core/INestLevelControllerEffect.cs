using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface INestLevelControllerEffect : IEffect {
  int id { get; }
  void visitINestLevelControllerEffect(INestLevelControllerEffectVisitor visitor);
}
       
}
