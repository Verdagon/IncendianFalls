using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounteringUCMutSetEffect {
  int id { get; }
  void visit(ICounteringUCMutSetEffectVisitor visitor);
}

}
