using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISpeedRingStrongMutSetEffect {
  int id { get; }
  void visit(ISpeedRingStrongMutSetEffectVisitor visitor);
}

}
