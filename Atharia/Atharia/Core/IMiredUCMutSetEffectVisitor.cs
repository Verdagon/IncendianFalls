using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMiredUCMutSetEffectVisitor {
  void visitMiredUCMutSetCreateEffect(MiredUCMutSetCreateEffect effect);
  void visitMiredUCMutSetDeleteEffect(MiredUCMutSetDeleteEffect effect);
  void visitMiredUCMutSetAddEffect(MiredUCMutSetAddEffect effect);
  void visitMiredUCMutSetRemoveEffect(MiredUCMutSetRemoveEffect effect);
}
         
}
