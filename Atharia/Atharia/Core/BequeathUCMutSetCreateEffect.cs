using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BequeathUCMutSetCreateEffect : IBequeathUCMutSetEffect {
  public readonly int id;
  public BequeathUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBequeathUCMutSetEffect.id => id;
  public void visitIBequeathUCMutSetEffect(IBequeathUCMutSetEffectVisitor visitor) {
    visitor.visitBequeathUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBequeathUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
