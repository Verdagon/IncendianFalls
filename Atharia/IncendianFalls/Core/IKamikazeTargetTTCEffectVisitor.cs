using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeTargetTTCEffectVisitor {
  void visitKamikazeTargetTTCCreateEffect(KamikazeTargetTTCCreateEffect effect);
  void visitKamikazeTargetTTCDeleteEffect(KamikazeTargetTTCDeleteEffect effect);
}

}
