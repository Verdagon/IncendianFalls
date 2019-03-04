using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DecorativeTTCCreateEffect : IDecorativeTTCEffect {
  public readonly int id;
  public DecorativeTTCCreateEffect(int id) {
    this.id = id;
  }
  int IDecorativeTTCEffect.id => id;
  public void visit(IDecorativeTTCEffectVisitor visitor) {
    visitor.visitDecorativeTTCCreateEffect(this);
  }
}

}
