using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StoneTTCMutSetDeleteEffect : IStoneTTCMutSetEffect {
  public readonly int id;
  public StoneTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IStoneTTCMutSetEffect.id => id;
  public void visitIStoneTTCMutSetEffect(IStoneTTCMutSetEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
