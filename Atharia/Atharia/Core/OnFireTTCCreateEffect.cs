using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct OnFireTTCCreateEffect : IOnFireTTCEffect {
  public readonly int id;
  public readonly OnFireTTCIncarnation incarnation;
  public OnFireTTCCreateEffect(int id, OnFireTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IOnFireTTCEffect.id => id;
  public void visitIOnFireTTCEffect(IOnFireTTCEffectVisitor visitor) {
    visitor.visitOnFireTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
