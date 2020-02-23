using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBloodTTCMutSetEffect {
  int id { get; }
  void visit(IBloodTTCMutSetEffectVisitor visitor);
}

}
