using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SimplePresenceTriggerTTCMutSetAddEffect : ISimplePresenceTriggerTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SimplePresenceTriggerTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISimplePresenceTriggerTTCMutSetEffect.id => id;
  public void visit(ISimplePresenceTriggerTTCMutSetEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCMutSetAddEffect(this);
  }
}

}
