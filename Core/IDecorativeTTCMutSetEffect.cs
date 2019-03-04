using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDecorativeTTCMutSetEffect {
  int id { get; }
  void visit(IDecorativeTTCMutSetEffectVisitor visitor);
}

}
