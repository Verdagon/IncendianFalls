using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GrassTTCDeleteEffect : IGrassTTCEffect {
  public readonly int id;
  public GrassTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IGrassTTCEffect.id => id;
  public void visit(IGrassTTCEffectVisitor visitor) {
    visitor.visitGrassTTCDeleteEffect(this);
  }
}

}
