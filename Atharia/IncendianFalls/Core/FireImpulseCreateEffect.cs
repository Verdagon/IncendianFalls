using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireImpulseCreateEffect : IFireImpulseEffect {
  public readonly int id;
  public readonly FireImpulseIncarnation incarnation;
  public FireImpulseCreateEffect(int id, FireImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IFireImpulseEffect.id => id;
  public void visitIFireImpulseEffect(IFireImpulseEffectVisitor visitor) {
    visitor.visitFireImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
