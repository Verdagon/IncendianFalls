using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BlazeRodCreateEffect : IBlazeRodEffect {
  public readonly int id;
  public readonly BlazeRodIncarnation incarnation;
  public BlazeRodCreateEffect(int id, BlazeRodIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBlazeRodEffect.id => id;
  public void visitIBlazeRodEffect(IBlazeRodEffectVisitor visitor) {
    visitor.visitBlazeRodCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlazeRodEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
