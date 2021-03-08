using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISlowRodStrongMutSetEffectVisitor {
  void visitSlowRodStrongMutSetCreateEffect(SlowRodStrongMutSetCreateEffect effect);
  void visitSlowRodStrongMutSetDeleteEffect(SlowRodStrongMutSetDeleteEffect effect);
  void visitSlowRodStrongMutSetAddEffect(SlowRodStrongMutSetAddEffect effect);
  void visitSlowRodStrongMutSetRemoveEffect(SlowRodStrongMutSetRemoveEffect effect);
}
         
}
