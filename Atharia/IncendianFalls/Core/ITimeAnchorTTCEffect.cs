using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITimeAnchorTTCEffect {
  int id { get; }
  void visit(ITimeAnchorTTCEffectVisitor visitor);
}
       
}
