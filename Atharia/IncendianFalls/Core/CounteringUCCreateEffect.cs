using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CounteringUCCreateEffect : ICounteringUCEffect {
  public readonly int id;
  public readonly CounteringUCIncarnation incarnation;
  public CounteringUCCreateEffect(int id, CounteringUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICounteringUCEffect.id => id;
  public void visitICounteringUCEffect(ICounteringUCEffectVisitor visitor) {
    visitor.visitCounteringUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCEffect(this);
  }
}

}
