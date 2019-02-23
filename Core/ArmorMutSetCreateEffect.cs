using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorMutSetCreateEffect : IArmorMutSetEffect {
  public readonly int id;
  public readonly ArmorMutSetIncarnation incarnation;
  public ArmorMutSetCreateEffect(
      int id,
      ArmorMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IArmorMutSetEffect.id => id;
  public void visit(IArmorMutSetEffectVisitor visitor) {
    visitor.visitArmorMutSetCreateEffect(this);
  }
}

}
