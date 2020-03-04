using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFloorTTCMutSetEffect {
  int id { get; }
  void visit(IFloorTTCMutSetEffectVisitor visitor);
}

}
