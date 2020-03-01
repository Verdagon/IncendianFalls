using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct OverlayCreateEffect : IOverlayEffect {
  public readonly int id;
  public OverlayCreateEffect(int id) {
    this.id = id;
  }
  int IOverlayEffect.id => id;
  public void visit(IOverlayEffectVisitor visitor) {
    visitor.visitOverlayCreateEffect(this);
  }
}

}
