using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMiredUCMutSetEffect {
  int id { get; }
  void visit(IMiredUCMutSetEffectVisitor visitor);
}

}
