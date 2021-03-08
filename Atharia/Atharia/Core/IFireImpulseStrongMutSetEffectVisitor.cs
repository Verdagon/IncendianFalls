using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireImpulseStrongMutSetEffectVisitor {
  void visitFireImpulseStrongMutSetCreateEffect(FireImpulseStrongMutSetCreateEffect effect);
  void visitFireImpulseStrongMutSetDeleteEffect(FireImpulseStrongMutSetDeleteEffect effect);
  void visitFireImpulseStrongMutSetAddEffect(FireImpulseStrongMutSetAddEffect effect);
  void visitFireImpulseStrongMutSetRemoveEffect(FireImpulseStrongMutSetRemoveEffect effect);
}
         
}
