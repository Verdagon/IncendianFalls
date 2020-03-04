using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMarkerTTCMutSetEffect {
  int id { get; }
  void visit(IMarkerTTCMutSetEffectVisitor visitor);
}

}
