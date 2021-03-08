using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveTTCMutSetEffect : IEffect {
  int id { get; }
  void visitICaveTTCMutSetEffect(ICaveTTCMutSetEffectVisitor visitor);
}

}
