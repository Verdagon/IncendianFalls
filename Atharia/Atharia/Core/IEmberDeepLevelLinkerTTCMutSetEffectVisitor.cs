using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEmberDeepLevelLinkerTTCMutSetEffectVisitor {
  void visitEmberDeepLevelLinkerTTCMutSetCreateEffect(EmberDeepLevelLinkerTTCMutSetCreateEffect effect);
  void visitEmberDeepLevelLinkerTTCMutSetDeleteEffect(EmberDeepLevelLinkerTTCMutSetDeleteEffect effect);
  void visitEmberDeepLevelLinkerTTCMutSetAddEffect(EmberDeepLevelLinkerTTCMutSetAddEffect effect);
  void visitEmberDeepLevelLinkerTTCMutSetRemoveEffect(EmberDeepLevelLinkerTTCMutSetRemoveEffect effect);
}
         
}
