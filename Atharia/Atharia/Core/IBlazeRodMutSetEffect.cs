using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlazeRodMutSetEffect : IEffect {
  int id { get; }
  void visitIBlazeRodMutSetEffect(IBlazeRodMutSetEffectVisitor visitor);
}

}
