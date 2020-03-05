using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseDefenseUCEffect {
  int id { get; }
  void visit(IBaseDefenseUCEffectVisitor visitor);
}
       
}
