using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LeafTTCDeleteEffect : ILeafTTCEffect {
  public readonly int id;
  public LeafTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ILeafTTCEffect.id => id;
  public void visitILeafTTCEffect(ILeafTTCEffectVisitor visitor) {
    visitor.visitLeafTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLeafTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
