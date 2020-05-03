using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Domino {
  public class Vector3Animation : IVector3Animation {
    IFloatAnimation x;
    IFloatAnimation y;
    IFloatAnimation z;

    public Vector3Animation(
        IFloatAnimation x,
        IFloatAnimation y,
        IFloatAnimation z) {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public Vector3 Get(long timeMs) {
      return new Vector3(x.Get(timeMs), y.Get(timeMs), z.Get(timeMs));
    }

    public IVector3Animation Simplify(long timeMs) {
      x = x.Simplify(timeMs);
      y = y.Simplify(timeMs);
      z = z.Simplify(timeMs);

      if (x is ConstantVector3Animation &&
          y is ConstantVector3Animation &&
          z is ConstantVector3Animation) {
        return new ConstantVector3Animation(Get(timeMs));
      }

      return this;
    }
  }
}
