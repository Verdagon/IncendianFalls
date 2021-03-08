using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISpeedRingMutSetEffectVisitor {
  void visitSpeedRingMutSetCreateEffect(SpeedRingMutSetCreateEffect effect);
  void visitSpeedRingMutSetDeleteEffect(SpeedRingMutSetDeleteEffect effect);
  void visitSpeedRingMutSetAddEffect(SpeedRingMutSetAddEffect effect);
  void visitSpeedRingMutSetRemoveEffect(SpeedRingMutSetRemoveEffect effect);
}
         
}
