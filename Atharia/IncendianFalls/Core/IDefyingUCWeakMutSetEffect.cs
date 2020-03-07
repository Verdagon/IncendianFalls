using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyingUCWeakMutSetEffect {
  int id { get; }
  void visit(IDefyingUCWeakMutSetEffectVisitor visitor);
}

}
