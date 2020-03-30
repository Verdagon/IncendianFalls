using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SorcerousUCCreateEffect : ISorcerousUCEffect {
  public readonly int id;
  public readonly SorcerousUCIncarnation incarnation;
  public SorcerousUCCreateEffect(int id, SorcerousUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ISorcerousUCEffect.id => id;
  public void visitISorcerousUCEffect(ISorcerousUCEffectVisitor visitor) {
    visitor.visitSorcerousUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSorcerousUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
