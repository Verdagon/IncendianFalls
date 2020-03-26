using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitIKamikazeAICapabilityUCEffect(IKamikazeAICapabilityUCEffectVisitor visitor);
}
       
}
