using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LeafTTCMutSetCreateEffect : ILeafTTCMutSetEffect {
  public readonly int id;
  public LeafTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ILeafTTCMutSetEffect.id => id;
  public void visitILeafTTCMutSetEffect(ILeafTTCMutSetEffectVisitor visitor) {
    visitor.visitLeafTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLeafTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
