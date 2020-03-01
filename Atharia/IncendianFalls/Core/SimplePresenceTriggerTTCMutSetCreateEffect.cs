using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SimplePresenceTriggerTTCMutSetCreateEffect : ISimplePresenceTriggerTTCMutSetEffect {
  public readonly int id;
  public SimplePresenceTriggerTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISimplePresenceTriggerTTCMutSetEffect.id => id;
  public void visit(ISimplePresenceTriggerTTCMutSetEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCMutSetCreateEffect(this);
  }
}

}
