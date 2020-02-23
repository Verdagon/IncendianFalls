using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICliffTTCMutSetEffectVisitor {
  void visitCliffTTCMutSetCreateEffect(CliffTTCMutSetCreateEffect effect);
  void visitCliffTTCMutSetDeleteEffect(CliffTTCMutSetDeleteEffect effect);
  void visitCliffTTCMutSetAddEffect(CliffTTCMutSetAddEffect effect);
  void visitCliffTTCMutSetRemoveEffect(CliffTTCMutSetRemoveEffect effect);
}
         
}
