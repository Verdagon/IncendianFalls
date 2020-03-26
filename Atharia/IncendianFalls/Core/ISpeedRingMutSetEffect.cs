using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISpeedRingMutSetEffect : IEffect {
  int id { get; }
  void visitISpeedRingMutSetEffect(ISpeedRingMutSetEffectVisitor visitor);
}

}
