using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStaircaseTTCMutSetEffect {
  int id { get; }
  void visit(IDownStaircaseTTCMutSetEffectVisitor visitor);
}

}
