using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFireBombTTCEffect {
  int id { get; }
  void visit(IFireBombTTCEffectVisitor visitor);
}
       
}
