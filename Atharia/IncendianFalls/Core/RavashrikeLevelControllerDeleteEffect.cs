using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RavashrikeLevelControllerDeleteEffect : IRavashrikeLevelControllerEffect {
  public readonly int id;
  public RavashrikeLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IRavashrikeLevelControllerEffect.id => id;
  public void visitIRavashrikeLevelControllerEffect(IRavashrikeLevelControllerEffectVisitor visitor) {
    visitor.visitRavashrikeLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavashrikeLevelControllerEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
