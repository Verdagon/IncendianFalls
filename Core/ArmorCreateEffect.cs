using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ArmorCreateEffect : IArmorEffect {
  public readonly int id;
  public readonly ArmorIncarnation incarnation;
  public ArmorCreateEffect(
      int id,
      ArmorIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IArmorEffect.id => id;
  public void visit(IArmorEffectVisitor visitor) {
    visitor.visitArmorCreateEffect(this);
  }
}
       
}
