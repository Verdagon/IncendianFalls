using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGrassTTCEffect : IEffect {
  int id { get; }
  void visitIGrassTTCEffect(IGrassTTCEffectVisitor visitor);
}
       
}
