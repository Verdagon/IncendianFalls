using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseMovementTimeUCEffectVisitor {
  void visitBaseMovementTimeUCCreateEffect(BaseMovementTimeUCCreateEffect effect);
  void visitBaseMovementTimeUCDeleteEffect(BaseMovementTimeUCDeleteEffect effect);
}

}
