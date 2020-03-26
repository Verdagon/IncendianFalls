using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRavashrikeLevelControllerEffect : IEffect {
  int id { get; }
  void visitIRavashrikeLevelControllerEffect(IRavashrikeLevelControllerEffectVisitor visitor);
}
       
}
