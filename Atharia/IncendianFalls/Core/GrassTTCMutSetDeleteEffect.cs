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
  public void visit(IGrassTTCMutSetEffectVisitor visitor) {
    visitor.visitGrassTTCMutSetDeleteEffect(this);
  }
}

}
