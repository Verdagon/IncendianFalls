using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInertiaRingStrongMutSetEffectVisitor {
  void visitInertiaRingStrongMutSetCreateEffect(InertiaRingStrongMutSetCreateEffect effect);
  void visitInertiaRingStrongMutSetDeleteEffect(InertiaRingStrongMutSetDeleteEffect effect);
  void visitInertiaRingStrongMutSetAddEffect(InertiaRingStrongMutSetAddEffect effect);
  void visitInertiaRingStrongMutSetRemoveEffect(InertiaRingStrongMutSetRemoveEffect effect);
}
         
}
