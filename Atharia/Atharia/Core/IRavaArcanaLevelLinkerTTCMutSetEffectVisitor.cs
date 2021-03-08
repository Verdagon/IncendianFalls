using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRavaArcanaLevelLinkerTTCMutSetEffectVisitor {
  void visitRavaArcanaLevelLinkerTTCMutSetCreateEffect(RavaArcanaLevelLinkerTTCMutSetCreateEffect effect);
  void visitRavaArcanaLevelLinkerTTCMutSetDeleteEffect(RavaArcanaLevelLinkerTTCMutSetDeleteEffect effect);
  void visitRavaArcanaLevelLinkerTTCMutSetAddEffect(RavaArcanaLevelLinkerTTCMutSetAddEffect effect);
  void visitRavaArcanaLevelLinkerTTCMutSetRemoveEffect(RavaArcanaLevelLinkerTTCMutSetRemoveEffect effect);
}
         
}
