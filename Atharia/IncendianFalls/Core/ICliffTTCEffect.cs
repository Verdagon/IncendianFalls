using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICliffTTCEffect {
  int id { get; }
  void visit(ICliffTTCEffectVisitor visitor);
}
       
}
