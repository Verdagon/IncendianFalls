using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInertiaRingStrongMutSetEffect {
  int id { get; }
  void visit(IInertiaRingStrongMutSetEffectVisitor visitor);
}

}
