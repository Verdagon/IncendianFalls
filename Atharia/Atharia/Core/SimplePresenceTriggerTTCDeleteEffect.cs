using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SimplePresenceTriggerTTCDeleteEffect : ISimplePresenceTriggerTTCEffect {
  public readonly int id;
  public SimplePresenceTriggerTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ISimplePresenceTriggerTTCEffect.id => id;
  public void visitISimplePresenceTriggerTTCEffect(ISimplePresenceTriggerTTCEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
