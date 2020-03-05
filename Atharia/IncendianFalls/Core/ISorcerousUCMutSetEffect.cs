using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISorcerousUCMutSetEffect {
  int id { get; }
  void visit(ISorcerousUCMutSetEffectVisitor visitor);
}

}
