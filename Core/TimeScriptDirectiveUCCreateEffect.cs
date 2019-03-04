using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TimeScriptDirectiveUCCreateEffect : ITimeScriptDirectiveUCEffect {
  public readonly int id;
  public TimeScriptDirectiveUCCreateEffect(int id) {
    this.id = id;
  }
  int ITimeScriptDirectiveUCEffect.id => id;
  public void visit(ITimeScriptDirectiveUCEffectVisitor visitor) {
    visitor.visitTimeScriptDirectiveUCCreateEffect(this);
  }
}

}
