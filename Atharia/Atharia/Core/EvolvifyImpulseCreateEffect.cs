using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EvolvifyImpulseCreateEffect : IEvolvifyImpulseEffect {
  public readonly int id;
  public readonly EvolvifyImpulseIncarnation incarnation;
  public EvolvifyImpulseCreateEffect(int id, EvolvifyImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IEvolvifyImpulseEffect.id => id;
  public void visitIEvolvifyImpulseEffect(IEvolvifyImpulseEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
