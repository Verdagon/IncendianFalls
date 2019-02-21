using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface ILevelMutSetEffect {
  int id { get; }
  void visit(ILevelMutSetEffectVisitor visitor);
}

}
