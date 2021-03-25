using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class Rand {
  private int rand;
  public int Next() {
    int result = new Random(rand).Next();
    rand = result;
    return result;
  }
  public int Next(int min, int max) {
    int randomThing = new Random(rand).Next();
    rand = randomThing;
    return randomThing % (max + 1 - min) + min;
  }
  public float Next(float min, float max, int numPossibilitiesBetween) {
    int r = Next(0, numPossibilitiesBetween);
    return min + (float)r / numPossibilitiesBetween * (max - min);
  }
}
}
