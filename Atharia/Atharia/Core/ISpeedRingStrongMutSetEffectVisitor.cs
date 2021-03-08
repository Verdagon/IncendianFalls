using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISpeedRingStrongMutSetEffectVisitor {
  void visitSpeedRingStrongMutSetCreateEffect(SpeedRingStrongMutSetCreateEffect effect);
  void visitSpeedRingStrongMutSetDeleteEffect(SpeedRingStrongMutSetDeleteEffect effect);
  void visitSpeedRingStrongMutSetAddEffect(SpeedRingStrongMutSetAddEffect effect);
  void visitSpeedRingStrongMutSetRemoveEffect(SpeedRingStrongMutSetRemoveEffect effect);
}
         
}
