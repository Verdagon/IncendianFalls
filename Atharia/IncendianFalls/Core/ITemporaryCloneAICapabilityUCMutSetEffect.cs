using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITemporaryCloneAICapabilityUCMutSetEffect {
  int id { get; }
  void visit(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor);
}

}
