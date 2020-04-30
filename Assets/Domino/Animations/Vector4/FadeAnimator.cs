
namespace Domino {
  static class FadeAnimator {
    public static IVector4Animation Fade(IVector4Animation animation, long startTimeMs, long durationMs) {

      IFloatAnimation redAnimation = new Vector4ComponentAnimation(animation, 0);
      IFloatAnimation greenAnimation = new Vector4ComponentAnimation(animation, 1);
      IFloatAnimation blueAnimation = new Vector4ComponentAnimation(animation, 2);
      IFloatAnimation alphaAnimation = new Vector4ComponentAnimation(animation, 3);

      IFloatAnimation newAlphaAnimation =
        new MultiplyFloatAnimation(
          alphaAnimation,
          new ClampFloatAnimation(
              startTimeMs,
              startTimeMs + durationMs,
              new LinearFloatAnimation(startTimeMs, 1.0f, -1.0f / durationMs)));

      return new Vector4Animation(redAnimation, greenAnimation, blueAnimation, newAlphaAnimation);
    }


    public static IVector4Animation FadeInThenOut(IVector4Animation animation, long startTimeMs, long inDurationMs, long outDurationMs) {

      IFloatAnimation redAnimation = new Vector4ComponentAnimation(animation, 0);
      IFloatAnimation greenAnimation = new Vector4ComponentAnimation(animation, 1);
      IFloatAnimation blueAnimation = new Vector4ComponentAnimation(animation, 2);
      IFloatAnimation alphaAnimation = new Vector4ComponentAnimation(animation, 3);

      IFloatAnimation newAlphaAnimation =
          new MultiplyFloatAnimation(
              alphaAnimation,
              new ClampFloatAnimation(
                  startTimeMs, startTimeMs + inDurationMs + outDurationMs,
                  new ThenFloatAnimation(
                      startTimeMs + inDurationMs,
                      new LinearFloatAnimation(startTimeMs, 0.0f, 1.0f / inDurationMs),
                      new LinearFloatAnimation(startTimeMs + inDurationMs, 1.0f, -1.0f / outDurationMs))));

      return new Vector4Animation(redAnimation, greenAnimation, blueAnimation, newAlphaAnimation);
    }
  }
}
