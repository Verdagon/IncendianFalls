using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FallsTTCMutSetAddEffect : IFallsTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FallsTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFallsTTCMutSetEffect.id => id;
  public void visit(IFallsTTCMutSetEffectVisitor visitor) {
    visitor.visitFallsTTCMutSetAddEffect(this);
  }
}

}
