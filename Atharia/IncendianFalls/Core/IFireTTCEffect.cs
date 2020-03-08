using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFireTTCEffect {
  int id { get; }
  void visit(IFireTTCEffectVisitor visitor);
}
       
}
