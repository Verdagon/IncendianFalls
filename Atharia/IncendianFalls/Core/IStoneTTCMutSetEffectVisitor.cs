using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStoneTTCMutSetEffectVisitor {
  void visitStoneTTCMutSetCreateEffect(StoneTTCMutSetCreateEffect effect);
  void visitStoneTTCMutSetDeleteEffect(StoneTTCMutSetDeleteEffect effect);
  void visitStoneTTCMutSetAddEffect(StoneTTCMutSetAddEffect effect);
  void visitStoneTTCMutSetRemoveEffect(StoneTTCMutSetRemoveEffect effect);
}
         
}
