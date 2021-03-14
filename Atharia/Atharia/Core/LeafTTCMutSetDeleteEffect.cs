using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LeafTTCMutSetDeleteEffect : ILeafTTCMutSetEffect {
  public readonly int id;
  public LeafTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ILeafTTCMutSetEffect.id => id;
  public void visitILeafTTCMutSetEffect(ILeafTTCMutSetEffectVisitor visitor) {
    visitor.visitLeafTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLeafTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
