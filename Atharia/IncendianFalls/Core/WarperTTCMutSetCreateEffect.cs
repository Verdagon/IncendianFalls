using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WarperTTCMutSetCreateEffect : IWarperTTCMutSetEffect {
  public readonly int id;
  public WarperTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IWarperTTCMutSetEffect.id => id;
  public void visit(IWarperTTCMutSetEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetCreateEffect(this);
  }
}

}
