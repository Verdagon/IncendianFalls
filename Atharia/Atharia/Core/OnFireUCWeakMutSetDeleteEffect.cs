using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireUCWeakMutSetDeleteEffect : IOnFireUCWeakMutSetEffect {
  public readonly int id;
  public OnFireUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IOnFireUCWeakMutSetEffect.id => id;
  public void visitIOnFireUCWeakMutSetEffect(IOnFireUCWeakMutSetEffectVisitor visitor) {
    visitor.visitOnFireUCWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
