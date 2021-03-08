using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMarkerTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIMarkerTTCMutSetEffect(IMarkerTTCMutSetEffectVisitor visitor);
}

}
