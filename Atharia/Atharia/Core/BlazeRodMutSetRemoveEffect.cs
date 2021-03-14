using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlazeRodMutSetRemoveEffect : IBlazeRodMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BlazeRodMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBlazeRodMutSetEffect.id => id;
  public void visitIBlazeRodMutSetEffect(IBlazeRodMutSetEffectVisitor visitor) {
    visitor.visitBlazeRodMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
