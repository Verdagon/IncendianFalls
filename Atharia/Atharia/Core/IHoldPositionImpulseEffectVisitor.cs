using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHoldPositionImpulseEffectVisitor {
  void visitHoldPositionImpulseCreateEffect(HoldPositionImpulseCreateEffect effect);
  void visitHoldPositionImpulseDeleteEffect(HoldPositionImpulseDeleteEffect effect);
}

}
