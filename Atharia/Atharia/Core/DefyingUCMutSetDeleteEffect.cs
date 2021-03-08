using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCMutSetDeleteEffect : IDefyingUCMutSetEffect {
  public readonly int id;
  public DefyingUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDefyingUCMutSetEffect.id => id;
  public void visitIDefyingUCMutSetEffect(IDefyingUCMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
