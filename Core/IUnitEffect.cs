using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEffect {
  int id { get; }
  void visit(IUnitEffectVisitor visitor);
}
       
}
