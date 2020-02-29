using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDirtTTCEffect {
  int id { get; }
  void visit(IDirtTTCEffectVisitor visitor);
}
       
}
