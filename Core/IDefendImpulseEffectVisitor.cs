using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefendImpulseEffectVisitor {
  void visitDefendImpulseCreateEffect(DefendImpulseCreateEffect effect);
  void visitDefendImpulseDeleteEffect(DefendImpulseDeleteEffect effect);
}

}
