using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFlowerTTCEffect : IEffect {
  int id { get; }
  void visitIFlowerTTCEffect(IFlowerTTCEffectVisitor visitor);
}
       
}
