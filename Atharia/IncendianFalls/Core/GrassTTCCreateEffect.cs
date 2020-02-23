using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GrassTTCCreateEffect : IGrassTTCEffect {
  public readonly int id;
  public GrassTTCCreateEffect(int id) {
    this.id = id;
  }
  int IGrassTTCEffect.id => id;
  public void visit(IGrassTTCEffectVisitor visitor) {
    visitor.visitGrassTTCCreateEffect(this);
  }
}

}
