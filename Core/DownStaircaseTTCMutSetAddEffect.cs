using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStaircaseTTCMutSetAddEffect : IDownStaircaseTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DownStaircaseTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDownStaircaseTTCMutSetEffect.id => id;
  public void visit(IDownStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStaircaseTTCMutSetAddEffect(this);
  }
}

}
