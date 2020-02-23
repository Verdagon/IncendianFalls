using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpstairsTTCMutSetAddEffect : IUpstairsTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UpstairsTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUpstairsTTCMutSetEffect.id => id;
  public void visit(IUpstairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpstairsTTCMutSetAddEffect(this);
  }
}

}
