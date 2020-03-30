using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCWeakMutSetRemoveEffect : IDefyingUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DefyingUCWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDefyingUCWeakMutSetEffect.id => id;
  public void visitIDefyingUCWeakMutSetEffect(IDefyingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyingUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
