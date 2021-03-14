using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlazeRodStrongMutSetDeleteEffect : IBlazeRodStrongMutSetEffect {
  public readonly int id;
  public BlazeRodStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBlazeRodStrongMutSetEffect.id => id;
  public void visitIBlazeRodStrongMutSetEffect(IBlazeRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlazeRodStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
