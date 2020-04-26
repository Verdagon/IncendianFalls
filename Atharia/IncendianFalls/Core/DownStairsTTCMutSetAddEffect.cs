using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStairsTTCMutSetAddEffect : IDownStairsTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DownStairsTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDownStairsTTCMutSetEffect.id => id;
  public void visitIDownStairsTTCMutSetEffect(IDownStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
