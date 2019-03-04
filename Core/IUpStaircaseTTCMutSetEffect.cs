using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStaircaseTTCMutSetEffect {
  int id { get; }
  void visit(IUpStaircaseTTCMutSetEffectVisitor visitor);
}

}
