using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInertiaRingMutSetEffectVisitor {
  void visitInertiaRingMutSetCreateEffect(InertiaRingMutSetCreateEffect effect);
  void visitInertiaRingMutSetDeleteEffect(InertiaRingMutSetDeleteEffect effect);
  void visitInertiaRingMutSetAddEffect(InertiaRingMutSetAddEffect effect);
  void visitInertiaRingMutSetRemoveEffect(InertiaRingMutSetRemoveEffect effect);
}
         
}
