using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFallsTTCMutSetEffect {
  int id { get; }
  void visit(IFallsTTCMutSetEffectVisitor visitor);
}

}
