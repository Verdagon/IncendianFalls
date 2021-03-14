using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvolvifyImpulseEffectVisitor {
  void visitEvolvifyImpulseCreateEffect(EvolvifyImpulseCreateEffect effect);
  void visitEvolvifyImpulseDeleteEffect(EvolvifyImpulseDeleteEffect effect);
}

}
