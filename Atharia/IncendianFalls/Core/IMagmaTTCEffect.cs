using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMagmaTTCEffect {
  int id { get; }
  void visit(IMagmaTTCEffectVisitor visitor);
}
       
}
