using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyImpulseEffectVisitor {
  void visitDefyImpulseCreateEffect(DefyImpulseCreateEffect effect);
  void visitDefyImpulseDeleteEffect(DefyImpulseDeleteEffect effect);
}

}
