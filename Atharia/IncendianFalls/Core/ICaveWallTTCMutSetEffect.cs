using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveWallTTCMutSetEffect {
  int id { get; }
  void visit(ICaveWallTTCMutSetEffectVisitor visitor);
}

}
