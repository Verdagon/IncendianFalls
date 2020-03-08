using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDoomedUCWeakMutSetEffect {
  int id { get; }
  void visit(IDoomedUCWeakMutSetEffectVisitor visitor);
}

}
