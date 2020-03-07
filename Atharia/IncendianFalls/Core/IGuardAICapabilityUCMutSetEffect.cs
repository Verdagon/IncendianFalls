using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGuardAICapabilityUCMutSetEffect {
  int id { get; }
  void visit(IGuardAICapabilityUCMutSetEffectVisitor visitor);
}

}
