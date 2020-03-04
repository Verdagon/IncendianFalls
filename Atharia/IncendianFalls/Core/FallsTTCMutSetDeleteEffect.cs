using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FallsTTCMutSetDeleteEffect : IFallsTTCMutSetEffect {
  public readonly int id;
  public FallsTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IFallsTTCMutSetEffect.id => id;
  public void visit(IFallsTTCMutSetEffectVisitor visitor) {
    visitor.visitFallsTTCMutSetDeleteEffect(this);
  }
}

}