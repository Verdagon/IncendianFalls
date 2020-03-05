using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISummonAICapabilityUCMutSetEffect {
  int id { get; }
  void visit(ISummonAICapabilityUCMutSetEffectVisitor visitor);
}

}
