using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseMovementTimeUCMutSetEffect {
  int id { get; }
  void visit(IBaseMovementTimeUCMutSetEffectVisitor visitor);
}

}
