using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPreActingUCWeakMutBunchCreateEffect : IIPreActingUCWeakMutBunchEffect {
  public readonly int id;
  public IPreActingUCWeakMutBunchCreateEffect(int id) {
    this.id = id;
  }
  int IIPreActingUCWeakMutBunchEffect.id => id;
  public void visit(IIPreActingUCWeakMutBunchEffectVisitor visitor) {
    visitor.visitIPreActingUCWeakMutBunchCreateEffect(this);
  }
}

}
