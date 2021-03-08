using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FallsTTCCreateEffect : IFallsTTCEffect {
  public readonly int id;
  public readonly FallsTTCIncarnation incarnation;
  public FallsTTCCreateEffect(int id, FallsTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IFallsTTCEffect.id => id;
  public void visitIFallsTTCEffect(IFallsTTCEffectVisitor visitor) {
    visitor.visitFallsTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFallsTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
