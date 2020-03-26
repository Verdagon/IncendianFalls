using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RavashrikeLevelControllerCreateEffect : IRavashrikeLevelControllerEffect {
  public readonly int id;
  public readonly RavashrikeLevelControllerIncarnation incarnation;
  public RavashrikeLevelControllerCreateEffect(int id, RavashrikeLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IRavashrikeLevelControllerEffect.id => id;
  public void visitIRavashrikeLevelControllerEffect(IRavashrikeLevelControllerEffectVisitor visitor) {
    visitor.visitRavashrikeLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavashrikeLevelControllerEffect(this);
  }
}

}
