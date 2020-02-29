using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDirtTTCMutSetEffect {
  int id { get; }
  void visit(IDirtTTCMutSetEffectVisitor visitor);
}

}
