using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LeafTTCCreateEffect : ILeafTTCEffect {
  public readonly int id;
  public readonly LeafTTCIncarnation incarnation;
  public LeafTTCCreateEffect(int id, LeafTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILeafTTCEffect.id => id;
  public void visitILeafTTCEffect(ILeafTTCEffectVisitor visitor) {
    visitor.visitLeafTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLeafTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
