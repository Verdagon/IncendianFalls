using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITemporaryCloneImpulseStrongMutSetEffectVisitor {
  void visitTemporaryCloneImpulseStrongMutSetCreateEffect(TemporaryCloneImpulseStrongMutSetCreateEffect effect);
  void visitTemporaryCloneImpulseStrongMutSetDeleteEffect(TemporaryCloneImpulseStrongMutSetDeleteEffect effect);
  void visitTemporaryCloneImpulseStrongMutSetAddEffect(TemporaryCloneImpulseStrongMutSetAddEffect effect);
  void visitTemporaryCloneImpulseStrongMutSetRemoveEffect(TemporaryCloneImpulseStrongMutSetRemoveEffect effect);
}
         
}
