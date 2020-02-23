using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRavaNestTTCMutSetEffectVisitor {
  void visitRavaNestTTCMutSetCreateEffect(RavaNestTTCMutSetCreateEffect effect);
  void visitRavaNestTTCMutSetDeleteEffect(RavaNestTTCMutSetDeleteEffect effect);
  void visitRavaNestTTCMutSetAddEffect(RavaNestTTCMutSetAddEffect effect);
  void visitRavaNestTTCMutSetRemoveEffect(RavaNestTTCMutSetRemoveEffect effect);
}
         
}
