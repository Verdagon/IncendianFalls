using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LeafTTCMutSetRemoveEffect : ILeafTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LeafTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILeafTTCMutSetEffect.id => id;
  public void visitILeafTTCMutSetEffect(ILeafTTCMutSetEffectVisitor visitor) {
    visitor.visitLeafTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLeafTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
