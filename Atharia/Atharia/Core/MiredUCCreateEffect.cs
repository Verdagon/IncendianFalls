using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MiredUCCreateEffect : IMiredUCEffect {
  public readonly int id;
  public readonly MiredUCIncarnation incarnation;
  public MiredUCCreateEffect(int id, MiredUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMiredUCEffect.id => id;
  public void visitIMiredUCEffect(IMiredUCEffectVisitor visitor) {
    visitor.visitMiredUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMiredUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
