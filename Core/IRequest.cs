using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRequest {
  string DStr();
  int GetDeterministicHashCode();
  void Visit(IRequestVisitor visitor);
}

}
