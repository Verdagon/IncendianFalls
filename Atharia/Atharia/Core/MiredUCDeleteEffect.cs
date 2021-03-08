using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MiredUCDeleteEffect : IMiredUCEffect {
  public readonly int id;
  public MiredUCDeleteEffect(int id) {
    this.id = id;
  }
  int IMiredUCEffect.id => id;
  public void visitIMiredUCEffect(IMiredUCEffectVisitor visitor) {
    visitor.visitMiredUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMiredUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
