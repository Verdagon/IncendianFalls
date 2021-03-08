using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnleashBideImpulseStrongMutSetEffectVisitor {
  void visitUnleashBideImpulseStrongMutSetCreateEffect(UnleashBideImpulseStrongMutSetCreateEffect effect);
  void visitUnleashBideImpulseStrongMutSetDeleteEffect(UnleashBideImpulseStrongMutSetDeleteEffect effect);
  void visitUnleashBideImpulseStrongMutSetAddEffect(UnleashBideImpulseStrongMutSetAddEffect effect);
  void visitUnleashBideImpulseStrongMutSetRemoveEffect(UnleashBideImpulseStrongMutSetRemoveEffect effect);
}
         
}
