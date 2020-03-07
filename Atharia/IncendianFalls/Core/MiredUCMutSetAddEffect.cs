using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCMutSetAddEffect : IMiredUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MiredUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMiredUCMutSetEffect.id => id;
  public void visit(IMiredUCMutSetEffectVisitor visitor) {
    visitor.visitMiredUCMutSetAddEffect(this);
  }
}

}
