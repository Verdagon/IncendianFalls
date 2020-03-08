using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeAICapabilityUCEffect {
  int id { get; }
  void visit(IKamikazeAICapabilityUCEffectVisitor visitor);
}
       
}
