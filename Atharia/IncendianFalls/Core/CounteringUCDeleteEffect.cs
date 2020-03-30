using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CounteringUCDeleteEffect : ICounteringUCEffect {
  public readonly int id;
  public CounteringUCDeleteEffect(int id) {
    this.id = id;
  }
  int ICounteringUCEffect.id => id;
  public void visitICounteringUCEffect(ICounteringUCEffectVisitor visitor) {
    visitor.visitCounteringUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
