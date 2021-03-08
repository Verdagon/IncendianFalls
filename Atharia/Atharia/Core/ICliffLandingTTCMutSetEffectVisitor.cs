using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICliffLandingTTCMutSetEffectVisitor {
  void visitCliffLandingTTCMutSetCreateEffect(CliffLandingTTCMutSetCreateEffect effect);
  void visitCliffLandingTTCMutSetDeleteEffect(CliffLandingTTCMutSetDeleteEffect effect);
  void visitCliffLandingTTCMutSetAddEffect(CliffLandingTTCMutSetAddEffect effect);
  void visitCliffLandingTTCMutSetRemoveEffect(CliffLandingTTCMutSetRemoveEffect effect);
}
         
}
