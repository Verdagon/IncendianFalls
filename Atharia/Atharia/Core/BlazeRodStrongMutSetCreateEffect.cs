using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlazeRodStrongMutSetCreateEffect : IBlazeRodStrongMutSetEffect {
  public readonly int id;
  public BlazeRodStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBlazeRodStrongMutSetEffect.id => id;
  public void visitIBlazeRodStrongMutSetEffect(IBlazeRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlazeRodStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
