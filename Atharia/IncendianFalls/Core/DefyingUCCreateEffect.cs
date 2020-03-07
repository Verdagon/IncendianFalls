using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefyingUCCreateEffect : IDefyingUCEffect {
  public readonly int id;
  public DefyingUCCreateEffect(int id) {
    this.id = id;
  }
  int IDefyingUCEffect.id => id;
  public void visit(IDefyingUCEffectVisitor visitor) {
    visitor.visitDefyingUCCreateEffect(this);
  }
}

}
