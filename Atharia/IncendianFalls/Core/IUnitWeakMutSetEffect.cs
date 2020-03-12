using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitWeakMutSetEffect {
  int id { get; }
  void visit(IUnitWeakMutSetEffectVisitor visitor);
}

}
