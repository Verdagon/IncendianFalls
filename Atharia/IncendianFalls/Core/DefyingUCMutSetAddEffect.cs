using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCMutSetAddEffect : IDefyingUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DefyingUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDefyingUCMutSetEffect.id => id;
  public void visitIDefyingUCMutSetEffect(IDefyingUCMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
