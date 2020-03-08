using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IObsidianFloorTTCMutSetEffectVisitor {
  void visitObsidianFloorTTCMutSetCreateEffect(ObsidianFloorTTCMutSetCreateEffect effect);
  void visitObsidianFloorTTCMutSetDeleteEffect(ObsidianFloorTTCMutSetDeleteEffect effect);
  void visitObsidianFloorTTCMutSetAddEffect(ObsidianFloorTTCMutSetAddEffect effect);
  void visitObsidianFloorTTCMutSetRemoveEffect(ObsidianFloorTTCMutSetRemoveEffect effect);
}
         
}
