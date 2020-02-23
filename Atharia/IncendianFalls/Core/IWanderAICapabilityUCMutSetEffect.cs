using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWanderAICapabilityUCMutSetEffect {
  int id { get; }
  void visit(IWanderAICapabilityUCMutSetEffectVisitor visitor);
}

}
