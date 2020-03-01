using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IOverlayEffect {
  int id { get; }
  void visit(IOverlayEffectVisitor visitor);
}
       
}
