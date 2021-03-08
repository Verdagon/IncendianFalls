using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISummonImpulseEffectVisitor {
  void visitSummonImpulseCreateEffect(SummonImpulseCreateEffect effect);
  void visitSummonImpulseDeleteEffect(SummonImpulseDeleteEffect effect);
}

}
