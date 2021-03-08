using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFloorTTCMutSetEffectVisitor {
  void visitFloorTTCMutSetCreateEffect(FloorTTCMutSetCreateEffect effect);
  void visitFloorTTCMutSetDeleteEffect(FloorTTCMutSetDeleteEffect effect);
  void visitFloorTTCMutSetAddEffect(FloorTTCMutSetAddEffect effect);
  void visitFloorTTCMutSetRemoveEffect(FloorTTCMutSetRemoveEffect effect);
}
         
}
