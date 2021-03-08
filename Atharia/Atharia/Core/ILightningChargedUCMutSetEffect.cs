using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargedUCMutSetEffect : IEffect {
  int id { get; }
  void visitILightningChargedUCMutSetEffect(ILightningChargedUCMutSetEffectVisitor visitor);
}

}
