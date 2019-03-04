using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStaircaseTTCMutSetAddEffect : IUpStaircaseTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UpStaircaseTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUpStaircaseTTCMutSetEffect.id => id;
  public void visit(IUpStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStaircaseTTCMutSetAddEffect(this);
  }
}

}
