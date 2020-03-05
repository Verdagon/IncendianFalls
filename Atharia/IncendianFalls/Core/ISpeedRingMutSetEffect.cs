using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISpeedRingMutSetEffect {
  int id { get; }
  void visit(ISpeedRingMutSetEffectVisitor visitor);
}

}
