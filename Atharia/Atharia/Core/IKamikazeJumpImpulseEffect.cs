using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeJumpImpulseEffect : IEffect {
  int id { get; }
  void visitIKamikazeJumpImpulseEffect(IKamikazeJumpImpulseEffectVisitor visitor);
}
       
}
