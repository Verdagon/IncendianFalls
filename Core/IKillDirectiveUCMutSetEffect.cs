using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKillDirectiveUCMutSetEffect {
  int id { get; }
  void visit(IKillDirectiveUCMutSetEffectVisitor visitor);
}

}
