using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILightningChargedUCEffect : IEffect {
  int id { get; }
  void visitILightningChargedUCEffect(ILightningChargedUCEffectVisitor visitor);
}
       
}
