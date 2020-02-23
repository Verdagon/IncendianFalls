using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGrassTTCEffect {
  int id { get; }
  void visit(IGrassTTCEffectVisitor visitor);
}
       
}
