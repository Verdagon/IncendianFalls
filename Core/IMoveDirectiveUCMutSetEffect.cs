using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMoveDirectiveUCMutSetEffect {
  int id { get; }
  void visit(IMoveDirectiveUCMutSetEffectVisitor visitor);
}

}
