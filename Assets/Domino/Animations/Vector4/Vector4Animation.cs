using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Domino {
  public class Vector4Animation : IVector4Animation {
    public static readonly Vector4Animation BLACK = Color(0, 0, 0, 1);
    public static readonly Vector4Animation WHITE = Color(1, 1, 1, 1);
    public static readonly Vector4Animation RED = Color(1, 0, 0, 1);
    public static readonly Vector4Animation GREEN = Color(0, 1, 0, 1);
    public static readonly Vector4Animation BLUE = Color(0, 0, 1, 1);
    public static readonly Vector4Animation GLOWY_WHITE = Color(1, 1, 1, 1.5f);

    IFloatAnimation x;
    IFloatAnimation y;
    IFloatAnimation z;
    IFloatAnimation w;

    public Vector4Animation(
        IFloatAnimation x,
        IFloatAnimation y,
        IFloatAnimation z,
        IFloatAnimation w) {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = w;
    }

    public static Vector4Animation Color(float r, float g, float b, float a) {
      return new Vector4Animation(
        new ConstantFloatAnimation(r),
        new ConstantFloatAnimation(g),
        new ConstantFloatAnimation(b),
        new ConstantFloatAnimation(a));
    }

    public static Vector4Animation Color(float r, float g, float b) {
      return Color(r, g, b, 1);
    }

    public Vector4 Get(long timeMs) {
      return new Vector4(x.Get(timeMs), y.Get(timeMs), z.Get(timeMs), w.Get(timeMs));
    }

    public IVector4Animation Simplify(long timeMs) {
      x = x.Simplify(timeMs);
      y = y.Simplify(timeMs);
      z = z.Simplify(timeMs);
      w = w.Simplify(timeMs);

      if (x is ConstantVector4Animation &&
          y is ConstantVector4Animation &&
          z is ConstantVector4Animation &&
          w is ConstantVector4Animation) {
        return new ConstantVector4Animation(Get(timeMs));
      }

      return this;
    }
  }
}
