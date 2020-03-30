using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCMutSetCreateEffect : IDefyingUCMutSetEffect {
  public readonly int id;
  public DefyingUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDefyingUCMutSetEffect.id => id;
  public void visitIDefyingUCMutSetEffect(IDefyingUCMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
