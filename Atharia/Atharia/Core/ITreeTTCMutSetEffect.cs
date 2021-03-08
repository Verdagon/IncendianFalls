using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITreeTTCMutSetEffect : IEffect {
  int id { get; }
  void visitITreeTTCMutSetEffect(ITreeTTCMutSetEffectVisitor visitor);
}

}
