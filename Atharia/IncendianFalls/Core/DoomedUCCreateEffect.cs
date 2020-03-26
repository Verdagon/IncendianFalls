using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DoomedUCCreateEffect : IDoomedUCEffect {
  public readonly int id;
  public readonly DoomedUCIncarnation incarnation;
  public DoomedUCCreateEffect(int id, DoomedUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDoomedUCEffect.id => id;
  public void visitIDoomedUCEffect(IDoomedUCEffectVisitor visitor) {
    visitor.visitDoomedUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDoomedUCEffect(this);
  }
}

}
