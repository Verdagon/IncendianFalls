using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMarkerTTCEffect {
  int id { get; }
  void visit(IMarkerTTCEffectVisitor visitor);
}
       
}
