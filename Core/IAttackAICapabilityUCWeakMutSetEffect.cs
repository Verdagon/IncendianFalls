using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackAICapabilityUCWeakMutSetEffect {
  int id { get; }
  void visit(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor);
}

}
