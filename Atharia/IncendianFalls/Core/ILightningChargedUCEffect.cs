using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILightningChargedUCEffect {
  int id { get; }
  void visit(ILightningChargedUCEffectVisitor visitor);
}
       
}
