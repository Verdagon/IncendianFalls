using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCMutSetDeleteEffect : ICounteringUCMutSetEffect {
  public readonly int id;
  public CounteringUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICounteringUCMutSetEffect.id => id;
  public void visitICounteringUCMutSetEffect(ICounteringUCMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
