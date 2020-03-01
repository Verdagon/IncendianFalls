using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SimplePresenceTriggerTTCMutSetDeleteEffect : ISimplePresenceTriggerTTCMutSetEffect {
  public readonly int id;
  public SimplePresenceTriggerTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ISimplePresenceTriggerTTCMutSetEffect.id => id;
  public void visit(ISimplePresenceTriggerTTCMutSetEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCMutSetDeleteEffect(this);
  }
}

}
