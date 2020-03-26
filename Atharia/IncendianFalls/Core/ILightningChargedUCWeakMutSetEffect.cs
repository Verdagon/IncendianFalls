using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargedUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitILightningChargedUCWeakMutSetEffect(ILightningChargedUCWeakMutSetEffectVisitor visitor);
}

}
