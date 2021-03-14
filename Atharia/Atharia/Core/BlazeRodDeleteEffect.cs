using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BlazeRodDeleteEffect : IBlazeRodEffect {
  public readonly int id;
  public BlazeRodDeleteEffect(int id) {
    this.id = id;
  }
  int IBlazeRodEffect.id => id;
  public void visitIBlazeRodEffect(IBlazeRodEffectVisitor visitor) {
    visitor.visitBlazeRodDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
