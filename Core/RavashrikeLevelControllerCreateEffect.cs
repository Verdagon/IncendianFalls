using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RavashrikeLevelControllerCreateEffect : IRavashrikeLevelControllerEffect {
  public readonly int id;
  public RavashrikeLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IRavashrikeLevelControllerEffect.id => id;
  public void visit(IRavashrikeLevelControllerEffectVisitor visitor) {
    visitor.visitRavashrikeLevelControllerCreateEffect(this);
  }
}

}
