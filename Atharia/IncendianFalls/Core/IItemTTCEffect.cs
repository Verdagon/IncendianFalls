using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IItemTTCEffect : IEffect {
  int id { get; }
  void visitIItemTTCEffect(IItemTTCEffectVisitor visitor);
}
       
}
