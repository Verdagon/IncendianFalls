using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTTCMutSetCreateEffect : IDecorativeTTCMutSetEffect {
  public readonly int id;
  public DecorativeTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDecorativeTTCMutSetEffect.id => id;
  public void visit(IDecorativeTTCMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTTCMutSetCreateEffect(this);
  }
}

}
