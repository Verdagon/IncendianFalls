using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISlowRodMutSetEffectVisitor {
  void visitSlowRodMutSetCreateEffect(SlowRodMutSetCreateEffect effect);
  void visitSlowRodMutSetDeleteEffect(SlowRodMutSetDeleteEffect effect);
  void visitSlowRodMutSetAddEffect(SlowRodMutSetAddEffect effect);
  void visitSlowRodMutSetRemoveEffect(SlowRodMutSetRemoveEffect effect);
}
         
}
