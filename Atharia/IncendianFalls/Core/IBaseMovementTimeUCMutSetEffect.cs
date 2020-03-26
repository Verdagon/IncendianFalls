using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseMovementTimeUCMutSetEffect : IEffect {
  int id { get; }
  void visitIBaseMovementTimeUCMutSetEffect(IBaseMovementTimeUCMutSetEffectVisitor visitor);
}

}
