using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GrassTTCMutSetAddEffect : IGrassTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public GrassTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IGrassTTCMutSetEffect.id => id;
  public void visit(IGrassTTCMutSetEffectVisitor visitor) {
    visitor.visitGrassTTCMutSetAddEffect(this);
  }
}

}
