using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;

public class ScaleAnimator : MonoBehaviour {
  private IClock clock;

  private IVector3Animation animation;

  public static ScaleAnimator MakeOrGetFrom(IClock clock, GameObject gameObject) {
    var animator = gameObject.GetComponent<ScaleAnimator>() as ScaleAnimator;
    if (animator == null) {
      animator = gameObject.AddComponent<ScaleAnimator>() as ScaleAnimator;
      animator.Init(clock);
    }
    return animator;
  }

  public void Init(IClock clock) {
    this.clock = clock;
    this.animation = new ConstantVector3Animation(transform.localScale);
  }

  public IVector3Animation Get() {
    return animation;
  }

  public void Set(IVector3Animation newAnimation) {
    Asserts.Assert(newAnimation != null);
    animation = newAnimation;

    UpdateScale();
  }

  public void Update() {
    UpdateScale();
  }

  private void UpdateScale() {
    Asserts.Assert(animation != null, "No animation??");
    Asserts.Assert(clock != null, "No animation??");
    transform.localScale = animation.Get(clock.GetTimeMs());

    if (animation is ConstantVector3Animation || animation is IdentityVector3Animation) {
      Destroy(this);
    }
  }
}
