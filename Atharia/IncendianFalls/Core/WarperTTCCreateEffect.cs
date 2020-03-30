using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WarperTTCCreateEffect : IWarperTTCEffect {
  public readonly int id;
  public readonly WarperTTCIncarnation incarnation;
  public WarperTTCCreateEffect(int id, WarperTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IWarperTTCEffect.id => id;
  public void visitIWarperTTCEffect(IWarperTTCEffectVisitor visitor) {
    visitor.visitWarperTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWarperTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
