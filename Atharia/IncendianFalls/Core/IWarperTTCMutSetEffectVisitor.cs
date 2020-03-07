using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWarperTTCMutSetEffectVisitor {
  void visitWarperTTCMutSetCreateEffect(WarperTTCMutSetCreateEffect effect);
  void visitWarperTTCMutSetDeleteEffect(WarperTTCMutSetDeleteEffect effect);
  void visitWarperTTCMutSetAddEffect(WarperTTCMutSetAddEffect effect);
  void visitWarperTTCMutSetRemoveEffect(WarperTTCMutSetRemoveEffect effect);
}
         
}
