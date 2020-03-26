using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SimplePresenceTriggerTTCIncarnation : ISimplePresenceTriggerTTCEffectVisitor {
  public readonly string name;
  public SimplePresenceTriggerTTCIncarnation(
      string name) {
    this.name = name;
  }
  public SimplePresenceTriggerTTCIncarnation Copy() {
    return new SimplePresenceTriggerTTCIncarnation(
name    );
  }

  public void visitSimplePresenceTriggerTTCCreateEffect(SimplePresenceTriggerTTCCreateEffect e) {}
  public void visitSimplePresenceTriggerTTCDeleteEffect(SimplePresenceTriggerTTCDeleteEffect e) {}

  public void ApplyEffect(ISimplePresenceTriggerTTCEffect effect) { effect.visitISimplePresenceTriggerTTCEffect(this); }
}

}
