using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface INoImpulseEffectVisitor {
  void visitNoImpulseCreateEffect(NoImpulseCreateEffect effect);
  void visitNoImpulseDeleteEffect(NoImpulseDeleteEffect effect);
}

}
