using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCMutSetRemoveEffect : IDefyingUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DefyingUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDefyingUCMutSetEffect.id => id;
  public void visitIDefyingUCMutSetEffect(IDefyingUCMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetEffect(this);
  }
}

}
