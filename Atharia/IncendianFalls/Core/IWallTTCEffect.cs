using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWallTTCEffect {
  int id { get; }
  void visit(IWallTTCEffectVisitor visitor);
}
       
}
