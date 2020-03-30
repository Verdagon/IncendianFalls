using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireTTCCreateEffect : IFireTTCEffect {
  public readonly int id;
  public readonly FireTTCIncarnation incarnation;
  public FireTTCCreateEffect(int id, FireTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IFireTTCEffect.id => id;
  public void visitIFireTTCEffect(IFireTTCEffectVisitor visitor) {
    visitor.visitFireTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
