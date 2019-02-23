using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IShieldingUCMutSetEffect {
  int id { get; }
  void visit(IShieldingUCMutSetEffectVisitor visitor);
}

}
