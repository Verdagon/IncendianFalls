using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMarkerTTCEffect : IEffect {
  int id { get; }
  void visitIMarkerTTCEffect(IMarkerTTCEffectVisitor visitor);
}
       
}
