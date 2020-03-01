using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISimplePresenceTriggerTTCEffectVisitor {
  void visitSimplePresenceTriggerTTCCreateEffect(SimplePresenceTriggerTTCCreateEffect effect);
  void visitSimplePresenceTriggerTTCDeleteEffect(SimplePresenceTriggerTTCDeleteEffect effect);
}

}
