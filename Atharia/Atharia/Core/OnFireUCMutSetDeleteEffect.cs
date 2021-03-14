using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireUCMutSetDeleteEffect : IOnFireUCMutSetEffect {
  public readonly int id;
  public OnFireUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IOnFireUCMutSetEffect.id => id;
  public void visitIOnFireUCMutSetEffect(IOnFireUCMutSetEffectVisitor visitor) {
    visitor.visitOnFireUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
