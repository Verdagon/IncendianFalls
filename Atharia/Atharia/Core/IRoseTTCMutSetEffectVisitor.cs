using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRoseTTCMutSetEffectVisitor {
  void visitRoseTTCMutSetCreateEffect(RoseTTCMutSetCreateEffect effect);
  void visitRoseTTCMutSetDeleteEffect(RoseTTCMutSetDeleteEffect effect);
  void visitRoseTTCMutSetAddEffect(RoseTTCMutSetAddEffect effect);
  void visitRoseTTCMutSetRemoveEffect(RoseTTCMutSetRemoveEffect effect);
}
         
}
