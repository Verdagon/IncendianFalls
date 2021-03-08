using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveTTCMutSetEffectVisitor {
  void visitCaveTTCMutSetCreateEffect(CaveTTCMutSetCreateEffect effect);
  void visitCaveTTCMutSetDeleteEffect(CaveTTCMutSetDeleteEffect effect);
  void visitCaveTTCMutSetAddEffect(CaveTTCMutSetAddEffect effect);
  void visitCaveTTCMutSetRemoveEffect(CaveTTCMutSetRemoveEffect effect);
}
         
}
