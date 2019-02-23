using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPreActingUCMutBunchCreateEffect : IIPreActingUCMutBunchEffect {
  public readonly int id;
  public readonly IPreActingUCMutBunchIncarnation incarnation;
  public IPreActingUCMutBunchCreateEffect(
      int id,
      IPreActingUCMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIPreActingUCMutBunchEffect.id => id;
  public void visit(IIPreActingUCMutBunchEffectVisitor visitor) {
    visitor.visitIPreActingUCMutBunchCreateEffect(this);
  }
}
       
}
