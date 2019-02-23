using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMoveDirectiveUCEffectVisitor {
  void visitMoveDirectiveUCCreateEffect(MoveDirectiveUCCreateEffect effect);
  void visitMoveDirectiveUCDeleteEffect(MoveDirectiveUCDeleteEffect effect);
}

}
