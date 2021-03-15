using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BequeathUCMutSetAddEffect : IBequeathUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BequeathUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBequeathUCMutSetEffect.id => id;
  public void visitIBequeathUCMutSetEffect(IBequeathUCMutSetEffectVisitor visitor) {
    visitor.visitBequeathUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBequeathUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
