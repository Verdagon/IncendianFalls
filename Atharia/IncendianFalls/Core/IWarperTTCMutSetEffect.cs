using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWarperTTCMutSetEffect {
  int id { get; }
  void visit(IWarperTTCMutSetEffectVisitor visitor);
}

}
