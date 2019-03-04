using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeCloneAICapabilityUCMutSetEffect {
  int id { get; }
  void visit(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor);
}

}
