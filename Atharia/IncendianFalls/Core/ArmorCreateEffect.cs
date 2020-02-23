using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ArmorCreateEffect : IArmorEffect {
  public readonly int id;
  public ArmorCreateEffect(int id) {
    this.id = id;
  }
  int IArmorEffect.id => id;
  public void visit(IArmorEffectVisitor visitor) {
    visitor.visitArmorCreateEffect(this);
  }
}

}
