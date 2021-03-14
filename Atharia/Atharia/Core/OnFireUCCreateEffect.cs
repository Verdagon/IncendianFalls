using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct OnFireUCCreateEffect : IOnFireUCEffect {
  public readonly int id;
  public readonly OnFireUCIncarnation incarnation;
  public OnFireUCCreateEffect(int id, OnFireUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IOnFireUCEffect.id => id;
  public void visitIOnFireUCEffect(IOnFireUCEffectVisitor visitor) {
    visitor.visitOnFireUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
