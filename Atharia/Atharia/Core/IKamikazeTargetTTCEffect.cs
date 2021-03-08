using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeTargetTTCEffect : IEffect {
  int id { get; }
  void visitIKamikazeTargetTTCEffect(IKamikazeTargetTTCEffectVisitor visitor);
}
       
}
