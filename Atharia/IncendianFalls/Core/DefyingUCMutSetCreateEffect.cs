using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCMutSetCreateEffect : IDefyingUCMutSetEffect {
  public readonly int id;
  public DefyingUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDefyingUCMutSetEffect.id => id;
  public void visit(IDefyingUCMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetCreateEffect(this);
  }
}

}
