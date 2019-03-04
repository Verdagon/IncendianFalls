using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ItemTTCCreateEffect : IItemTTCEffect {
  public readonly int id;
  public ItemTTCCreateEffect(int id) {
    this.id = id;
  }
  int IItemTTCEffect.id => id;
  public void visit(IItemTTCEffectVisitor visitor) {
    visitor.visitItemTTCCreateEffect(this);
  }
}

}
