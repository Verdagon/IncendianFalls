using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMoveImpulseStrongMutSetEffectVisitor {
  void visitMoveImpulseStrongMutSetCreateEffect(MoveImpulseStrongMutSetCreateEffect effect);
  void visitMoveImpulseStrongMutSetDeleteEffect(MoveImpulseStrongMutSetDeleteEffect effect);
  void visitMoveImpulseStrongMutSetAddEffect(MoveImpulseStrongMutSetAddEffect effect);
  void visitMoveImpulseStrongMutSetRemoveEffect(MoveImpulseStrongMutSetRemoveEffect effect);
}
         
}
