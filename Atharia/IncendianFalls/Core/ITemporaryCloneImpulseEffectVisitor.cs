using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITemporaryCloneImpulseEffectVisitor {
  void visitTemporaryCloneImpulseCreateEffect(TemporaryCloneImpulseCreateEffect effect);
  void visitTemporaryCloneImpulseDeleteEffect(TemporaryCloneImpulseDeleteEffect effect);
}

}
