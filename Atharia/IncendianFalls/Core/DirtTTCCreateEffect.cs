using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DirtTTCCreateEffect : IDirtTTCEffect {
  public readonly int id;
  public readonly DirtTTCIncarnation incarnation;
  public DirtTTCCreateEffect(int id, DirtTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDirtTTCEffect.id => id;
  public void visitIDirtTTCEffect(IDirtTTCEffectVisitor visitor) {
    visitor.visitDirtTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDirtTTCEffect(this);
  }
}

}
