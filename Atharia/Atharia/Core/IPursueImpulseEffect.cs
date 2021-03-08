using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPursueImpulseEffect : IEffect {
  int id { get; }
  void visitIPursueImpulseEffect(IPursueImpulseEffectVisitor visitor);
}
       
}
