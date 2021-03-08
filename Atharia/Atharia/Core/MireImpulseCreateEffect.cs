using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MireImpulseCreateEffect : IMireImpulseEffect {
  public readonly int id;
  public readonly MireImpulseIncarnation incarnation;
  public MireImpulseCreateEffect(int id, MireImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMireImpulseEffect.id => id;
  public void visitIMireImpulseEffect(IMireImpulseEffectVisitor visitor) {
    visitor.visitMireImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMireImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
