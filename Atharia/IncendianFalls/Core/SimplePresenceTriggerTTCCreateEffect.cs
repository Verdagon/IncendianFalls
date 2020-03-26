using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SimplePresenceTriggerTTCCreateEffect : ISimplePresenceTriggerTTCEffect {
  public readonly int id;
  public readonly SimplePresenceTriggerTTCIncarnation incarnation;
  public SimplePresenceTriggerTTCCreateEffect(int id, SimplePresenceTriggerTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ISimplePresenceTriggerTTCEffect.id => id;
  public void visitISimplePresenceTriggerTTCEffect(ISimplePresenceTriggerTTCEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCEffect(this);
  }
}

}
