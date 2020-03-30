using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SimplePresenceTriggerTTCMutSetAddEffect : ISimplePresenceTriggerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SimplePresenceTriggerTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISimplePresenceTriggerTTCMutSetEffect.id => id;
  public void visitISimplePresenceTriggerTTCMutSetEffect(ISimplePresenceTriggerTTCMutSetEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSimplePresenceTriggerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
