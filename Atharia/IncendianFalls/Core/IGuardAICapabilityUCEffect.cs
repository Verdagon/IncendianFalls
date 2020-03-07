using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGuardAICapabilityUCEffect {
  int id { get; }
  void visit(IGuardAICapabilityUCEffectVisitor visitor);
}
       
}
