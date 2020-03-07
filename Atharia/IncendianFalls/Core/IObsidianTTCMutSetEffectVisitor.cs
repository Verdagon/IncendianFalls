using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IObsidianTTCMutSetEffectVisitor {
  void visitObsidianTTCMutSetCreateEffect(ObsidianTTCMutSetCreateEffect effect);
  void visitObsidianTTCMutSetDeleteEffect(ObsidianTTCMutSetDeleteEffect effect);
  void visitObsidianTTCMutSetAddEffect(ObsidianTTCMutSetAddEffect effect);
  void visitObsidianTTCMutSetRemoveEffect(ObsidianTTCMutSetRemoveEffect effect);
}
         
}
