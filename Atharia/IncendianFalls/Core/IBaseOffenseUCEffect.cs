using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseOffenseUCEffect {
  int id { get; }
  void visit(IBaseOffenseUCEffectVisitor visitor);
}
       
}