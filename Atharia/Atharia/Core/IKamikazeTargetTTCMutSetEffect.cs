using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeTargetTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffectVisitor visitor);
}

}
