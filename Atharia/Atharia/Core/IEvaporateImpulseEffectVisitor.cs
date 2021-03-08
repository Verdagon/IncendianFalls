using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvaporateImpulseEffectVisitor {
  void visitEvaporateImpulseCreateEffect(EvaporateImpulseCreateEffect effect);
  void visitEvaporateImpulseDeleteEffect(EvaporateImpulseDeleteEffect effect);
}

}
