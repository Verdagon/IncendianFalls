using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IPursueImpulseEffectVisitor {
  void visitPursueImpulseCreateEffect(PursueImpulseCreateEffect effect);
  void visitPursueImpulseDeleteEffect(PursueImpulseDeleteEffect effect);
}

}
