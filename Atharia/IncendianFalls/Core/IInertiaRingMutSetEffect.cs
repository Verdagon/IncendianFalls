using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInertiaRingMutSetEffect {
  int id { get; }
  void visit(IInertiaRingMutSetEffectVisitor visitor);
}

}
