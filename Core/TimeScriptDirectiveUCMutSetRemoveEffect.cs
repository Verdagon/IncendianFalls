using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeScriptDirectiveUCMutSetRemoveEffect : ITimeScriptDirectiveUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TimeScriptDirectiveUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITimeScriptDirectiveUCMutSetEffect.id => id;
  public void visit(ITimeScriptDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitTimeScriptDirectiveUCMutSetRemoveEffect(this);
  }
}

}
