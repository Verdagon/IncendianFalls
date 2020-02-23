using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
  IClock clock;
  IMatrix4x4Animation transformAnimation_ = new IdentityMatrix4x4Animation();

  public void Init(IClock clock) {
    this.clock = clock;
  }

  public IMatrix4x4Animation transformAnimation {
    get { return transformAnimation_; }
    set {
      transformAnimation_ = value;
      Update();
    }
  }

  void Update() {
    transformAnimation_ = transformAnimation_.Simplify(clock.GetTimeMs());
    if (transformAnimation_ is IdentityMatrix4x4Animation) {
      transform.FromMatrix(Matrix4x4.identity);
      Destroy(this);
      return;
    }
    transform.FromMatrix(transformAnimation_.Get(clock.GetTimeMs()));
  }
}
