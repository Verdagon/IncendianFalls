using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeScriptDirectiveUCMutSetCreateEffect : ITimeScriptDirectiveUCMutSetEffect {
  public readonly int id;
  public TimeScriptDirectiveUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITimeScriptDirectiveUCMutSetEffect.id => id;
  public void visit(ITimeScriptDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitTimeScriptDirectiveUCMutSetCreateEffect(this);
  }
}

}
