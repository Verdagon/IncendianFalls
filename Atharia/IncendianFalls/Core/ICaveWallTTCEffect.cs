using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICaveWallTTCEffect {
  int id { get; }
  void visit(ICaveWallTTCEffectVisitor visitor);
}
       
}
