using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyingUCMutSetEffect {
  int id { get; }
  void visit(IDefyingUCMutSetEffectVisitor visitor);
}

}
