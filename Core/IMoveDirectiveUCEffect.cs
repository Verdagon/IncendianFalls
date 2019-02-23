using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMoveDirectiveUCEffect {
  int id { get; }
  void visit(IMoveDirectiveUCEffectVisitor visitor);
}
       
}
