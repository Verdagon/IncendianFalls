using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlazeRodMutSetDeleteEffect : IBlazeRodMutSetEffect {
  public readonly int id;
  public BlazeRodMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBlazeRodMutSetEffect.id => id;
  public void visitIBlazeRodMutSetEffect(IBlazeRodMutSetEffectVisitor visitor) {
    visitor.visitBlazeRodMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
