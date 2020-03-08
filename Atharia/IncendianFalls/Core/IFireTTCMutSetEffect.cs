using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireTTCMutSetEffect {
  int id { get; }
  void visit(IFireTTCMutSetEffectVisitor visitor);
}

}
