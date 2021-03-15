using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDeathTriggerUCMutSetEffectVisitor {
  void visitDeathTriggerUCMutSetCreateEffect(DeathTriggerUCMutSetCreateEffect effect);
  void visitDeathTriggerUCMutSetDeleteEffect(DeathTriggerUCMutSetDeleteEffect effect);
  void visitDeathTriggerUCMutSetAddEffect(DeathTriggerUCMutSetAddEffect effect);
  void visitDeathTriggerUCMutSetRemoveEffect(DeathTriggerUCMutSetRemoveEffect effect);
}
         
}
