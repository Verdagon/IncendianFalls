using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IItemTTCMutSetEffect {
  int id { get; }
  void visit(IItemTTCMutSetEffectVisitor visitor);
}

}
