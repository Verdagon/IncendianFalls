using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RandCreateEffect : IRandEffect {
  public readonly int id;
  public readonly RandIncarnation incarnation;
  public RandCreateEffect(
      int id,
      RandIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IRandEffect.id => id;
  public void visit(IRandEffectVisitor visitor) {
    visitor.visitRandCreateEffect(this);
  }
}
       
}
