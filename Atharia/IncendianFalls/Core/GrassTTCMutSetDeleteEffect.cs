using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GrassTTCMutSetDeleteEffect : IGrassTTCMutSetEffect {
  public readonly int id;
  public GrassTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IGrassTTCMutSetEffect.id => id;
  public void visitIGrassTTCMutSetEffect(IGrassTTCMutSetEffectVisitor visitor) {
    visitor.visitGrassTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGrassTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
