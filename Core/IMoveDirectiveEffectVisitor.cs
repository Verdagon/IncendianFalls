using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IMoveDirectiveEffectVisitor {
  void visitMoveDirectiveCreateEffect(MoveDirectiveCreateEffect effect);
  void visitMoveDirectiveDeleteEffect(MoveDirectiveDeleteEffect effect);
}

}
