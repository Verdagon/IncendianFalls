using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct OverlayDeleteEffect : IOverlayEffect {
  public readonly int id;
  public OverlayDeleteEffect(int id) {
    this.id = id;
  }
  int IOverlayEffect.id => id;
  public void visit(IOverlayEffectVisitor visitor) {
    visitor.visitOverlayDeleteEffect(this);
  }
}

}
