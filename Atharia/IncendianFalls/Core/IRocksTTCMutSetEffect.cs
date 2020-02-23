using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRocksTTCMutSetEffect {
  int id { get; }
  void visit(IRocksTTCMutSetEffectVisitor visitor);
}

}
