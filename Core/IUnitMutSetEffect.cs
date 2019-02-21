using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitMutSetEffect {
  int id { get; }
  void visit(IUnitMutSetEffectVisitor visitor);
}

}
