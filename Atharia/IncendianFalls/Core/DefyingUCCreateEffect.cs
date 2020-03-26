using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefyingUCCreateEffect : IDefyingUCEffect {
  public readonly int id;
  public readonly DefyingUCIncarnation incarnation;
  public DefyingUCCreateEffect(int id, DefyingUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDefyingUCEffect.id => id;
  public void visitIDefyingUCEffect(IDefyingUCEffectVisitor visitor) {
    visitor.visitDefyingUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyingUCEffect(this);
  }
}

}
