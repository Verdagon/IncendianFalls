using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CommCreateEffect : ICommEffect {
  public readonly int id;
  public readonly CommIncarnation incarnation;
  public CommCreateEffect(int id, CommIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICommEffect.id => id;
  public void visitICommEffect(ICommEffectVisitor visitor) {
    visitor.visitCommCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCommEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
