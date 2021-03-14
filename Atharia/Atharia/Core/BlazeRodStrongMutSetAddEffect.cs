using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlazeRodStrongMutSetAddEffect : IBlazeRodStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BlazeRodStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBlazeRodStrongMutSetEffect.id => id;
  public void visitIBlazeRodStrongMutSetEffect(IBlazeRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlazeRodStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
