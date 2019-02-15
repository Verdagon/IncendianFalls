using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IRequest {
  string DStr();
  void Visit(IRequestVisitor visitor);
}

}
