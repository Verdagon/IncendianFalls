using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GrassTTCMutSetCreateEffect : IGrassTTCMutSetEffect {
  public readonly int id;
  public GrassTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IGrassTTCMutSetEffect.id => id;
  public void visit(IGrassTTCMutSetEffectVisitor visitor) {
    visitor.visitGrassTTCMutSetCreateEffect(this);
  }
}

}
