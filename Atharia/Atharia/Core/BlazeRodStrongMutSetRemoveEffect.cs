using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlazeRodStrongMutSetRemoveEffect : IBlazeRodStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BlazeRodStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBlazeRodStrongMutSetEffect.id => id;
  public void visitIBlazeRodStrongMutSetEffect(IBlazeRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlazeRodStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
