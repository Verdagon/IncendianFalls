using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICliffTTCMutSetEffect {
  int id { get; }
  void visit(ICliffTTCMutSetEffectVisitor visitor);
}

}
