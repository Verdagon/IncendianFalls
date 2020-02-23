using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRavaNestTTCMutSetEffect {
  int id { get; }
  void visit(IRavaNestTTCMutSetEffectVisitor visitor);
}

}
