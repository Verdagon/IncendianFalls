using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISimplePresenceTriggerTTCMutSetEffectVisitor {
  void visitSimplePresenceTriggerTTCMutSetCreateEffect(SimplePresenceTriggerTTCMutSetCreateEffect effect);
  void visitSimplePresenceTriggerTTCMutSetDeleteEffect(SimplePresenceTriggerTTCMutSetDeleteEffect effect);
  void visitSimplePresenceTriggerTTCMutSetAddEffect(SimplePresenceTriggerTTCMutSetAddEffect effect);
  void visitSimplePresenceTriggerTTCMutSetRemoveEffect(SimplePresenceTriggerTTCMutSetRemoveEffect effect);
}
         
}
