using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FallsTTCCreateEffect : IFallsTTCEffect {
  public readonly int id;
  public FallsTTCCreateEffect(int id) {
    this.id = id;
  }
  int IFallsTTCEffect.id => id;
  public void visit(IFallsTTCEffectVisitor visitor) {
    visitor.visitFallsTTCCreateEffect(this);
  }
}

}
