using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TimeScriptDirectiveUCDeleteEffect : ITimeScriptDirectiveUCEffect {
  public readonly int id;
  public TimeScriptDirectiveUCDeleteEffect(int id) {
    this.id = id;
  }
  int ITimeScriptDirectiveUCEffect.id => id;
  public void visit(ITimeScriptDirectiveUCEffectVisitor visitor) {
    visitor.visitTimeScriptDirectiveUCDeleteEffect(this);
  }
}

}
