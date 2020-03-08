using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IObsidianFloorTTCMutSetEffect {
  int id { get; }
  void visit(IObsidianFloorTTCMutSetEffectVisitor visitor);
}

}
