using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BequeathUCMutSetRemoveEffect : IBequeathUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BequeathUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBequeathUCMutSetEffect.id => id;
  public void visitIBequeathUCMutSetEffect(IBequeathUCMutSetEffectVisitor visitor) {
    visitor.visitBequeathUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBequeathUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
