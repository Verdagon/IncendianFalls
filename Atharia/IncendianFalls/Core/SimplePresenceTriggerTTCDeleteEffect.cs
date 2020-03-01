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
  public void visit(ISimplePresenceTriggerTTCEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCDeleteEffect(this);
  }
}

}
