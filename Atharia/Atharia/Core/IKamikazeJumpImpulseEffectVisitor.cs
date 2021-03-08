using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeJumpImpulseEffectVisitor {
  void visitKamikazeJumpImpulseCreateEffect(KamikazeJumpImpulseCreateEffect effect);
  void visitKamikazeJumpImpulseDeleteEffect(KamikazeJumpImpulseDeleteEffect effect);
}

}
