using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDeathTriggerUCEffectVisitor {
  void visitDeathTriggerUCCreateEffect(DeathTriggerUCCreateEffect effect);
  void visitDeathTriggerUCDeleteEffect(DeathTriggerUCDeleteEffect effect);
}

}
