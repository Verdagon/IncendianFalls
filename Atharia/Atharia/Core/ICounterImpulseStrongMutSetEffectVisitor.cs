using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounterImpulseStrongMutSetEffectVisitor {
  void visitCounterImpulseStrongMutSetCreateEffect(CounterImpulseStrongMutSetCreateEffect effect);
  void visitCounterImpulseStrongMutSetDeleteEffect(CounterImpulseStrongMutSetDeleteEffect effect);
  void visitCounterImpulseStrongMutSetAddEffect(CounterImpulseStrongMutSetAddEffect effect);
  void visitCounterImpulseStrongMutSetRemoveEffect(CounterImpulseStrongMutSetRemoveEffect effect);
}
         
}
