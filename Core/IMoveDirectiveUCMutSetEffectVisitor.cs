using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMoveDirectiveUCMutSetEffectVisitor {
  void visitMoveDirectiveUCMutSetCreateEffect(MoveDirectiveUCMutSetCreateEffect effect);
  void visitMoveDirectiveUCMutSetDeleteEffect(MoveDirectiveUCMutSetDeleteEffect effect);
  void visitMoveDirectiveUCMutSetAddEffect(MoveDirectiveUCMutSetAddEffect effect);
  void visitMoveDirectiveUCMutSetRemoveEffect(MoveDirectiveUCMutSetRemoveEffect effect);
}
         
}
