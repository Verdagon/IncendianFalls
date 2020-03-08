using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDoomedUCMutSetEffect {
  int id { get; }
  void visit(IDoomedUCMutSetEffectVisitor visitor);
}

}
