using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownstairsTTCMutSetRemoveEffect : IDownstairsTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DownstairsTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDownstairsTTCMutSetEffect.id => id;
  public void visit(IDownstairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownstairsTTCMutSetRemoveEffect(this);
  }
}

}
