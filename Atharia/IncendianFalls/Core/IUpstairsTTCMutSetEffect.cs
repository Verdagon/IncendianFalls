using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStairsTTCMutSetEffect {
  int id { get; }
  void visit(IUpStairsTTCMutSetEffectVisitor visitor);
}

}
