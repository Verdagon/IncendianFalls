using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIItemMutBunchEffect {
  int id { get; }
  void visit(IIItemMutBunchEffectVisitor visitor);
}

}
