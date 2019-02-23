using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPostActingUCMutBunchCreateEffect : IIPostActingUCMutBunchEffect {
  public readonly int id;
  public readonly IPostActingUCMutBunchIncarnation incarnation;
  public IPostActingUCMutBunchCreateEffect(
      int id,
      IPostActingUCMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIPostActingUCMutBunchEffect.id => id;
  public void visit(IIPostActingUCMutBunchEffectVisitor visitor) {
    visitor.visitIPostActingUCMutBunchCreateEffect(this);
  }
}
       
}
