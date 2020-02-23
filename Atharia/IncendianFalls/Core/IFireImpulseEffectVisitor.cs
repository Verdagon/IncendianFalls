using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireImpulseEffectVisitor {
  void visitFireImpulseCreateEffect(FireImpulseCreateEffect effect);
  void visitFireImpulseDeleteEffect(FireImpulseDeleteEffect effect);
}

}
