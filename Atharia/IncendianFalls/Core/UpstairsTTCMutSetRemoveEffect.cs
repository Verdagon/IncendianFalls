using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpstairsTTCMutSetRemoveEffect : IUpstairsTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UpstairsTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUpstairsTTCMutSetEffect.id => id;
  public void visit(IUpstairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpstairsTTCMutSetRemoveEffect(this);
  }
}

}
