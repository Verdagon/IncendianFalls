using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounterImpulseEffectVisitor {
  void visitCounterImpulseCreateEffect(CounterImpulseCreateEffect effect);
  void visitCounterImpulseDeleteEffect(CounterImpulseDeleteEffect effect);
}

}
