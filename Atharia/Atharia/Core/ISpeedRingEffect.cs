using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISpeedRingEffect : IEffect {
  int id { get; }
  void visitISpeedRingEffect(ISpeedRingEffectVisitor visitor);
}
       
}
