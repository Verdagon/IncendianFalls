using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStoneTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIStoneTTCMutSetEffect(IStoneTTCMutSetEffectVisitor visitor);
}

}
