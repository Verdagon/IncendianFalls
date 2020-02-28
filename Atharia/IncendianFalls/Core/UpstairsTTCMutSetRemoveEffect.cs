using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStairsTTCMutSetRemoveEffect : IUpStairsTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UpStairsTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUpStairsTTCMutSetEffect.id => id;
  public void visit(IUpStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStairsTTCMutSetRemoveEffect(this);
  }
}

}
