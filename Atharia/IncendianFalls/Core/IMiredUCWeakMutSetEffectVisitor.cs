using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMiredUCWeakMutSetEffectVisitor {
  void visitMiredUCWeakMutSetCreateEffect(MiredUCWeakMutSetCreateEffect effect);
  void visitMiredUCWeakMutSetDeleteEffect(MiredUCWeakMutSetDeleteEffect effect);
  void visitMiredUCWeakMutSetAddEffect(MiredUCWeakMutSetAddEffect effect);
  void visitMiredUCWeakMutSetRemoveEffect(MiredUCWeakMutSetRemoveEffect effect);
}
         
}
