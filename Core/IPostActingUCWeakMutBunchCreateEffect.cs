using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPostActingUCWeakMutBunchCreateEffect : IIPostActingUCWeakMutBunchEffect {
  public readonly int id;
  public readonly IPostActingUCWeakMutBunchIncarnation incarnation;
  public IPostActingUCWeakMutBunchCreateEffect(
      int id,
      IPostActingUCWeakMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIPostActingUCWeakMutBunchEffect.id => id;
  public void visit(IIPostActingUCWeakMutBunchEffectVisitor visitor) {
    visitor.visitIPostActingUCWeakMutBunchCreateEffect(this);
  }
}
       
}
