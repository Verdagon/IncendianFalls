using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IPursueImpulseStrongMutSetEffectVisitor {
  void visitPursueImpulseStrongMutSetCreateEffect(PursueImpulseStrongMutSetCreateEffect effect);
  void visitPursueImpulseStrongMutSetDeleteEffect(PursueImpulseStrongMutSetDeleteEffect effect);
  void visitPursueImpulseStrongMutSetAddEffect(PursueImpulseStrongMutSetAddEffect effect);
  void visitPursueImpulseStrongMutSetRemoveEffect(PursueImpulseStrongMutSetRemoveEffect effect);
}
         
}
