using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMudTTCEffect {
  int id { get; }
  void visit(IMudTTCEffectVisitor visitor);
}
       
}
