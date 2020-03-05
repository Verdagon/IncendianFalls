using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseMovementTimeUCMutSetEffectVisitor {
  void visitBaseMovementTimeUCMutSetCreateEffect(BaseMovementTimeUCMutSetCreateEffect effect);
  void visitBaseMovementTimeUCMutSetDeleteEffect(BaseMovementTimeUCMutSetDeleteEffect effect);
  void visitBaseMovementTimeUCMutSetAddEffect(BaseMovementTimeUCMutSetAddEffect effect);
  void visitBaseMovementTimeUCMutSetRemoveEffect(BaseMovementTimeUCMutSetRemoveEffect effect);
}
         
}
