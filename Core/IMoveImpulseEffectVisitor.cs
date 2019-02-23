using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMoveImpulseEffectVisitor {
  void visitMoveImpulseCreateEffect(MoveImpulseCreateEffect effect);
  void visitMoveImpulseDeleteEffect(MoveImpulseDeleteEffect effect);
}

}
