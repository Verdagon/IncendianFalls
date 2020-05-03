using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino {
  static class FloatAnimations {
    public static IFloatAnimation InThenOut(
        long startTimeMs,
        long inDurationMs,
        long outDurationMs,
        float start = 0,
        float middle = 1,
        float end = 0) {
      return new ClampFloatAnimation(
          startTimeMs, startTimeMs + inDurationMs + outDurationMs,
          new ThenFloatAnimation(
              startTimeMs + inDurationMs,
              new LinearFloatAnimation(startTimeMs, start, (middle - start) / inDurationMs),
              new LinearFloatAnimation(startTimeMs + inDurationMs, middle, (end - middle) / outDurationMs)));
    }
  }
}
