using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IItemStrongMutBunchCreateEffect : IIItemStrongMutBunchEffect {
  public readonly int id;
  public readonly IItemStrongMutBunchIncarnation incarnation;
  public IItemStrongMutBunchCreateEffect(int id, IItemStrongMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIItemStrongMutBunchEffect.id => id;
  public void visitIIItemStrongMutBunchEffect(IIItemStrongMutBunchEffectVisitor visitor) {
    visitor.visitIItemStrongMutBunchCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIItemStrongMutBunchEffect(this);
  }
}

}
