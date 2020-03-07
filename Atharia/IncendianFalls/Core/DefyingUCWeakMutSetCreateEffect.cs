using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCWeakMutSetCreateEffect : IDefyingUCWeakMutSetEffect {
  public readonly int id;
  public DefyingUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDefyingUCWeakMutSetEffect.id => id;
  public void visit(IDefyingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCWeakMutSetCreateEffect(this);
  }
}

}
