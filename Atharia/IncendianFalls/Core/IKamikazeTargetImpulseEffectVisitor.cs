using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeTargetImpulseEffectVisitor {
  void visitKamikazeTargetImpulseCreateEffect(KamikazeTargetImpulseCreateEffect effect);
  void visitKamikazeTargetImpulseDeleteEffect(KamikazeTargetImpulseDeleteEffect effect);
}

}
