using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCWeakMutSetAddEffect : IDefyingUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DefyingUCWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDefyingUCWeakMutSetEffect.id => id;
  public void visitIDefyingUCWeakMutSetEffect(IDefyingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyingUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
