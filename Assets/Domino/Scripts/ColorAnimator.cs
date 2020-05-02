using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;

public class ColorAnimator : MonoBehaviour {
  private IClock clock;

  private IVector4Animation animation;
  private RenderPriority renderPriority;

  public static ColorAnimator MakeOrGetFrom(IClock clock, GameObject gameObject) {
    var animator = gameObject.GetComponent<ColorAnimator>() as ColorAnimator;
    if (animator == null) {
      animator = gameObject.AddComponent<ColorAnimator>() as ColorAnimator;
      animator.Init(clock);
    }
    return animator;
  }

  public void Init(IClock clock) {
    this.clock = clock;
    this.animation = new ConstantVector4Animation(new Vector4(0, 0, 0, 1));

    // Make sure it exists.
    Asserts.Assert(GetComponent<ColorChanger>() != null);
  }

  public IVector4Animation Get() {
    return animation;
  }

  public void Set(IVector4Animation newAnimation, RenderPriority newRenderPriority) {
    Asserts.Assert(newAnimation != null);
    animation = newAnimation;
    renderPriority = newRenderPriority;

    UpdateColor();
  }

  public void Start() {
    Asserts.Assert(GetComponent<ColorChanger>() != null);
  }

  public void Update() {
    UpdateColor();
  }

  private void UpdateColor() {
    Asserts.Assert(GetComponent<ColorChanger>() != null, "No colorchanger on this thing!");
    Asserts.Assert(animation != null, "No animation??");
    Asserts.Assert(clock != null, "No animation??");
    GetComponent<ColorChanger>().Set(animation.Get(clock.GetTimeMs()), renderPriority);

    if (animation is ConstantVector4Animation || animation is IdentityVector4Animation) {
      Destroy(this);
    }
  }
}
