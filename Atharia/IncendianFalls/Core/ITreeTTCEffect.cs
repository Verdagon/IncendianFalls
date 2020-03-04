using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITreeTTCEffect {
  int id { get; }
  void visit(ITreeTTCEffectVisitor visitor);
}
       
}
