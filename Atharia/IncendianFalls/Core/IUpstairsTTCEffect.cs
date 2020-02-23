using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUpstairsTTCEffect {
  int id { get; }
  void visit(IUpstairsTTCEffectVisitor visitor);
}
       
}
