using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeScriptDirectiveUCMutSetDeleteEffect : ITimeScriptDirectiveUCMutSetEffect {
  public readonly int id;
  public TimeScriptDirectiveUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITimeScriptDirectiveUCMutSetEffect.id => id;
  public void visit(ITimeScriptDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitTimeScriptDirectiveUCMutSetDeleteEffect(this);
  }
}

}
