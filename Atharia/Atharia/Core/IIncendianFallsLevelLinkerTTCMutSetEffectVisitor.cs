using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIncendianFallsLevelLinkerTTCMutSetEffectVisitor {
  void visitIncendianFallsLevelLinkerTTCMutSetCreateEffect(IncendianFallsLevelLinkerTTCMutSetCreateEffect effect);
  void visitIncendianFallsLevelLinkerTTCMutSetDeleteEffect(IncendianFallsLevelLinkerTTCMutSetDeleteEffect effect);
  void visitIncendianFallsLevelLinkerTTCMutSetAddEffect(IncendianFallsLevelLinkerTTCMutSetAddEffect effect);
  void visitIncendianFallsLevelLinkerTTCMutSetRemoveEffect(IncendianFallsLevelLinkerTTCMutSetRemoveEffect effect);
}
         
}
