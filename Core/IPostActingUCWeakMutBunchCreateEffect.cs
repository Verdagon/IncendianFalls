using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPostActingUCWeakMutBunchCreateEffect : IIPostActingUCWeakMutBunchEffect {
  public readonly int id;
  public IPostActingUCWeakMutBunchCreateEffect(int id) {
    this.id = id;
  }
  int IIPostActingUCWeakMutBunchEffect.id => id;
  public void visit(IIPostActingUCWeakMutBunchEffectVisitor visitor) {
    visitor.visitIPostActingUCWeakMutBunchCreateEffect(this);
  }
}

}
