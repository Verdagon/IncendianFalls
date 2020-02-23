using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GrassTTCMutSetRemoveEffect : IGrassTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public GrassTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IGrassTTCMutSetEffect.id => id;
  public void visit(IGrassTTCMutSetEffectVisitor visitor) {
    visitor.visitGrassTTCMutSetRemoveEffect(this);
  }
}

}
