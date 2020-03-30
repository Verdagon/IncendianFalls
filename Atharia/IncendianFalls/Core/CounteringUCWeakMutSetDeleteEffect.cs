using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCWeakMutSetDeleteEffect : ICounteringUCWeakMutSetEffect {
  public readonly int id;
  public CounteringUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICounteringUCWeakMutSetEffect.id => id;
  public void visitICounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
