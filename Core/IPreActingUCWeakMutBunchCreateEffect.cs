using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPreActingUCWeakMutBunchCreateEffect : IIPreActingUCWeakMutBunchEffect {
  public readonly int id;
  public readonly IPreActingUCWeakMutBunchIncarnation incarnation;
  public IPreActingUCWeakMutBunchCreateEffect(
      int id,
      IPreActingUCWeakMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIPreActingUCWeakMutBunchEffect.id => id;
  public void visit(IIPreActingUCWeakMutBunchEffectVisitor visitor) {
    visitor.visitIPreActingUCWeakMutBunchCreateEffect(this);
  }
}
       
}
