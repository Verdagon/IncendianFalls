using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlazeRodMutSetAddEffect : IBlazeRodMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BlazeRodMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBlazeRodMutSetEffect.id => id;
  public void visitIBlazeRodMutSetEffect(IBlazeRodMutSetEffectVisitor visitor) {
    visitor.visitBlazeRodMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
