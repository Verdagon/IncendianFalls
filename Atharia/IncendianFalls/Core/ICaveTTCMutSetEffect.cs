using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveTTCMutSetEffect {
  int id { get; }
  void visit(ICaveTTCMutSetEffectVisitor visitor);
}

}
