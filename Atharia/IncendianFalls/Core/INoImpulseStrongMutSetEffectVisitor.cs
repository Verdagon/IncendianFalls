using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface INoImpulseStrongMutSetEffectVisitor {
  void visitNoImpulseStrongMutSetCreateEffect(NoImpulseStrongMutSetCreateEffect effect);
  void visitNoImpulseStrongMutSetDeleteEffect(NoImpulseStrongMutSetDeleteEffect effect);
  void visitNoImpulseStrongMutSetAddEffect(NoImpulseStrongMutSetAddEffect effect);
  void visitNoImpulseStrongMutSetRemoveEffect(NoImpulseStrongMutSetRemoveEffect effect);
}
         
}
