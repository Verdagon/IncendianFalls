using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWarperTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIWarperTTCMutSetEffect(IWarperTTCMutSetEffectVisitor visitor);
}

}
