using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICliffTTCEffect : IEffect {
  int id { get; }
  void visitICliffTTCEffect(ICliffTTCEffectVisitor visitor);
}
       
}
