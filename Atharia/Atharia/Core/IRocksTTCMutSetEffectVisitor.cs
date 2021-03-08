using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRocksTTCMutSetEffectVisitor {
  void visitRocksTTCMutSetCreateEffect(RocksTTCMutSetCreateEffect effect);
  void visitRocksTTCMutSetDeleteEffect(RocksTTCMutSetDeleteEffect effect);
  void visitRocksTTCMutSetAddEffect(RocksTTCMutSetAddEffect effect);
  void visitRocksTTCMutSetRemoveEffect(RocksTTCMutSetRemoveEffect effect);
}
         
}
