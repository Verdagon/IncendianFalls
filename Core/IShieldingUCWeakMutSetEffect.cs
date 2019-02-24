using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IShieldingUCWeakMutSetEffect {
  int id { get; }
  void visit(IShieldingUCWeakMutSetEffectVisitor visitor);
}

}
