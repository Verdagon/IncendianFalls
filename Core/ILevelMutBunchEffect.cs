using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ILevelMutBunchEffect {
  int id { get; }
  void visit(ILevelMutBunchEffectVisitor visitor);
}

}
