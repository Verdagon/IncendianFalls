using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SimplePresenceTriggerTTCCreateEffect : ISimplePresenceTriggerTTCEffect {
  public readonly int id;
  public SimplePresenceTriggerTTCCreateEffect(int id) {
    this.id = id;
  }
  int ISimplePresenceTriggerTTCEffect.id => id;
  public void visit(ISimplePresenceTriggerTTCEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCCreateEffect(this);
  }
}

}
