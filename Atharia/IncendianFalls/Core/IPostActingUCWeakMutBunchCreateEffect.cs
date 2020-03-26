using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPostActingUCWeakMutBunchCreateEffect : IIPostActingUCWeakMutBunchEffect {
  public readonly int id;
  public readonly IPostActingUCWeakMutBunchIncarnation incarnation;
  public IPostActingUCWeakMutBunchCreateEffect(int id, IPostActingUCWeakMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIPostActingUCWeakMutBunchEffect.id => id;
  public void visitIIPostActingUCWeakMutBunchEffect(IIPostActingUCWeakMutBunchEffectVisitor visitor) {
    visitor.visitIPostActingUCWeakMutBunchCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIPostActingUCWeakMutBunchEffect(this);
  }
}

}
