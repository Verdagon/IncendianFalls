using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStaircaseTTCMutSetRemoveEffect : IUpStaircaseTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UpStaircaseTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUpStaircaseTTCMutSetEffect.id => id;
  public void visit(IUpStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStaircaseTTCMutSetRemoveEffect(this);
  }
}

}
