using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICounteringUCEffect {
  int id { get; }
  void visit(ICounteringUCEffectVisitor visitor);
}
       
}
