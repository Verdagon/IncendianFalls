using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWaterTTCEffect : IEffect {
  int id { get; }
  void visitIWaterTTCEffect(IWaterTTCEffectVisitor visitor);
}
       
}
