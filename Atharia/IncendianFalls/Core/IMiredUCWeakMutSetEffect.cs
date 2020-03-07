using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMiredUCWeakMutSetEffect {
  int id { get; }
  void visit(IMiredUCWeakMutSetEffectVisitor visitor);
}

}
