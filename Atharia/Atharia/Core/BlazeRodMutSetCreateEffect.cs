using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlazeRodMutSetCreateEffect : IBlazeRodMutSetEffect {
  public readonly int id;
  public BlazeRodMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBlazeRodMutSetEffect.id => id;
  public void visitIBlazeRodMutSetEffect(IBlazeRodMutSetEffectVisitor visitor) {
    visitor.visitBlazeRodMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
