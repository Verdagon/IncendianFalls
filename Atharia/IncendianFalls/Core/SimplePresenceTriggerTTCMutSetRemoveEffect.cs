using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SimplePresenceTriggerTTCMutSetRemoveEffect : ISimplePresenceTriggerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SimplePresenceTriggerTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISimplePresenceTriggerTTCMutSetEffect.id => id;
  public void visitISimplePresenceTriggerTTCMutSetEffect(ISimplePresenceTriggerTTCMutSetEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
