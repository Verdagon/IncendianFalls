using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SorcerousUCMutSetDeleteEffect : ISorcerousUCMutSetEffect {
  public readonly int id;
  public SorcerousUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ISorcerousUCMutSetEffect.id => id;
  public void visit(ISorcerousUCMutSetEffectVisitor visitor) {
    visitor.visitSorcerousUCMutSetDeleteEffect(this);
  }
}

}
