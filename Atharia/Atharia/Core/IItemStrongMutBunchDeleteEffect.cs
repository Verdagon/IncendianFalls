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
  public void visitIIItemStrongMutBunchEffect(IIItemStrongMutBunchEffectVisitor visitor) {
    visitor.visitIItemStrongMutBunchDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIItemStrongMutBunchEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
