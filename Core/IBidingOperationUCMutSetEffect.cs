using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBidingOperationUCMutSetEffect {
  int id { get; }
  void visit(IBidingOperationUCMutSetEffectVisitor visitor);
}

}
