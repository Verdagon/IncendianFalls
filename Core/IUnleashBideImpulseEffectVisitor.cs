using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnleashBideImpulseEffectVisitor {
  void visitUnleashBideImpulseCreateEffect(UnleashBideImpulseCreateEffect effect);
  void visitUnleashBideImpulseDeleteEffect(UnleashBideImpulseDeleteEffect effect);
}

}
