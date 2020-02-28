using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IItemStrongMutBunchDeleteEffect : IIItemStrongMutBunchEffect {
  public readonly int id;
  public IItemStrongMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIItemStrongMutBunchEffect.id => id;
  public void visit(IIItemStrongMutBunchEffectVisitor visitor) {
    visitor.visitIItemStrongMutBunchDeleteEffect(this);
  }
}

}
