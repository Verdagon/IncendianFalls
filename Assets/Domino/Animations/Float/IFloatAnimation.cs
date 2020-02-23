using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloatAnimation {
  float Get(long timeMs);
  IFloatAnimation Simplify(long timeMs);
}
