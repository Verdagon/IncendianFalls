using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargedUCWeakMutSetEffect {
  int id { get; }
  void visit(ILightningChargedUCWeakMutSetEffectVisitor visitor);
}

}
