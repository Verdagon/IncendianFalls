using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBideImpulseEffectVisitor {
  void visitBideImpulseCreateEffect(BideImpulseCreateEffect effect);
  void visitBideImpulseDeleteEffect(BideImpulseDeleteEffect effect);
}

}
