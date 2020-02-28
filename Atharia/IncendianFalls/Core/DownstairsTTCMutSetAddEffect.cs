using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStairsTTCMutSetAddEffect : IDownStairsTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DownStairsTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDownStairsTTCMutSetEffect.id => id;
  public void visit(IDownStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetAddEffect(this);
  }
}

}
