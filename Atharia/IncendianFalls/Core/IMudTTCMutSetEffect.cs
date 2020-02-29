using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMudTTCMutSetEffect {
  int id { get; }
  void visit(IMudTTCMutSetEffectVisitor visitor);
}

}
