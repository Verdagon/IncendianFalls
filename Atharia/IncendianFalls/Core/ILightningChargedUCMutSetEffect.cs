using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargedUCMutSetEffect {
  int id { get; }
  void visit(ILightningChargedUCMutSetEffectVisitor visitor);
}

}
