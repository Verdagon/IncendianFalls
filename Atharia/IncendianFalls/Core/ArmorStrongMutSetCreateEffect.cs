using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorStrongMutSetCreateEffect : IArmorStrongMutSetEffect {
  public readonly int id;
  public ArmorStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IArmorStrongMutSetEffect.id => id;
  public void visit(IArmorStrongMutSetEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetCreateEffect(this);
  }
}

}
