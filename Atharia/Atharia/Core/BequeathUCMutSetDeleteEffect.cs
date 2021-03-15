using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BequeathUCMutSetDeleteEffect : IBequeathUCMutSetEffect {
  public readonly int id;
  public BequeathUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBequeathUCMutSetEffect.id => id;
  public void visitIBequeathUCMutSetEffect(IBequeathUCMutSetEffectVisitor visitor) {
    visitor.visitBequeathUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBequeathUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
