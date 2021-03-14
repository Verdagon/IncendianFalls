using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlazeRodStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIBlazeRodStrongMutSetEffect(IBlazeRodStrongMutSetEffectVisitor visitor);
}

}
