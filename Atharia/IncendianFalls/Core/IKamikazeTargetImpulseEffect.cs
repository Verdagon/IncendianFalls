using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeTargetImpulseEffect : IEffect {
  int id { get; }
  void visitIKamikazeTargetImpulseEffect(IKamikazeTargetImpulseEffectVisitor visitor);
}
       
}
