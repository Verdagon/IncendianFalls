using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCMutSetDeleteEffect : IMiredUCMutSetEffect {
  public readonly int id;
  public MiredUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMiredUCMutSetEffect.id => id;
  public void visitIMiredUCMutSetEffect(IMiredUCMutSetEffectVisitor visitor) {
    visitor.visitMiredUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMiredUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
