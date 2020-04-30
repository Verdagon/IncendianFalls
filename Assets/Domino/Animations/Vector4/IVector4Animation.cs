using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Domino {
  public interface IVector4Animation {
    Vector4 Get(long timeMs);
    IVector4Animation Simplify(long timeMs);
  }
}
