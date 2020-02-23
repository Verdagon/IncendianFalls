using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDownstairsTTCEffect {
  int id { get; }
  void visit(IDownstairsTTCEffectVisitor visitor);
}
       
}
