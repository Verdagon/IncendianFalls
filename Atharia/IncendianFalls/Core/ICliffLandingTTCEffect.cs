using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICliffLandingTTCEffect : IEffect {
  int id { get; }
  void visitICliffLandingTTCEffect(ICliffLandingTTCEffectVisitor visitor);
}
       
}
