using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IItemMutBunchDeleteEffect : IIItemMutBunchEffect {
  public readonly int id;
  public IItemMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIItemMutBunchEffect.id => id;
  public void visit(IIItemMutBunchEffectVisitor visitor) {
    visitor.visitIItemMutBunchDeleteEffect(this);
  }
}

}
