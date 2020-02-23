using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICliffLandingTTCEffect {
  int id { get; }
  void visit(ICliffLandingTTCEffectVisitor visitor);
}
       
}
