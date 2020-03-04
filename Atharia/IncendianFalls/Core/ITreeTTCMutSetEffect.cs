using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITreeTTCMutSetEffect {
  int id { get; }
  void visit(ITreeTTCMutSetEffectVisitor visitor);
}

}
