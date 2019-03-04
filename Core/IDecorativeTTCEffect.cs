using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDecorativeTTCEffect {
  int id { get; }
  void visit(IDecorativeTTCEffectVisitor visitor);
}
       
}
