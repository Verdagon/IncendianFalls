using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBloodTTCEffect {
  int id { get; }
  void visit(IBloodTTCEffectVisitor visitor);
}
       
}
