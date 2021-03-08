using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMireImpulseEffectVisitor {
  void visitMireImpulseCreateEffect(MireImpulseCreateEffect effect);
  void visitMireImpulseDeleteEffect(MireImpulseDeleteEffect effect);
}

}
