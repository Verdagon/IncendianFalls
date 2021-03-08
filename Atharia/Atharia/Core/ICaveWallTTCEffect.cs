using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICaveWallTTCEffect : IEffect {
  int id { get; }
  void visitICaveWallTTCEffect(ICaveWallTTCEffectVisitor visitor);
}
       
}
