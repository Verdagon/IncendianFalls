using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITreeTTCEffect : IEffect {
  int id { get; }
  void visitITreeTTCEffect(ITreeTTCEffectVisitor visitor);
}
       
}
