using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RandSetRandEffect : IRandEffect {
  public readonly int id;
  public readonly int newValue;
  public RandSetRandEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IRandEffect.id => id;

  public void visitIRandEffect(IRandEffectVisitor visitor) {
    visitor.visitRandSetRandEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRandEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
