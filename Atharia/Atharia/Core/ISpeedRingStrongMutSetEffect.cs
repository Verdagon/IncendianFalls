using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISpeedRingStrongMutSetEffect : IEffect {
  int id { get; }
  void visitISpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffectVisitor visitor);
}

}
