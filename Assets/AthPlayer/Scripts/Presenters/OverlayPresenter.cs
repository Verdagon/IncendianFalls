using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AthPlayer {
  class OverlayPresenter : IGameEffectObserver, IGameEffectVisitor {
    SlowableTimerClock clock;
    ISuperstructure ss;
    Game game;
    OverlayPanelView overlayPanelView;

    public OverlayPresenter(
        SlowableTimerClock clock,
        ISuperstructure ss,
        Game game,
        OverlayPanelView overlayPanelView) {
      this.clock = clock;
      this.ss = ss;
      this.game = game;
      this.overlayPanelView = overlayPanelView;

      game.AddObserver(this);

      overlayPanelView.OverlayClosed += OverlayClosed;
    }

    public void OnGameEffect(IGameEffect effect) { effect.visit(this); }
    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) { }
    public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
    public void visitGameSetLevelEffect(GameSetLevelEffect effect) { }
    public void visitGameSetOverlayEffect(GameSetOverlayEffect effect) {
      if (game.overlay.Exists()) {
        var overlay = game.overlay;
        var buttonTexts = new List<string>();
        foreach (var button in game.overlay.buttons) {
          buttonTexts.Add(button.label);
        }
        overlayPanelView.Init(
          overlay.sizePercent / 100f,
          overlay.backgroundColor.ToUnity(),
          overlay.fadeInEndMs / 1000f,
          overlay.fadeOutStartMs / 1000f,
          overlay.fadeOutEndMs / 1000f,

          overlay.text,
          overlay.textColor.ToUnity(),
          overlay.textFadeInStartMs / 1000f,
          overlay.textFadeInEndMs / 1000f,
          overlay.textFadeOutStartMs / 1000f,
          overlay.textFadeOutEndMs / 1000f,
          overlay.topAligned,
          overlay.leftAligned,

          buttonTexts);

        //overlayPanelView.Init(
        //  .55f, // game.overlay.sizePercent / 100f,
        //  new UnityEngine.Color(.5f, 0, 0, .9f), //game.overlay.backgroundColor.ToUnity(),
        //  "What is the meaning of life?", // game.overlay.overlayText,
        //  new UnityEngine.Color(1, 1, 1, 1), // game.overlay.overlayTextColor.ToUnity(),
        //  game.overlay.topAligned,
        //  game.overlay.leftAligned,
        //  0,//game.overlay.automaticDismissDelayMs,
        //  50,//game.overlay.fadeInMs,
        //  50,//game.overlay.fadeOutMs,
        //  new List<string>() { "butts", "more butts" });//buttonTexts);

        //overlayPanelView.Init(
        //  1f, // game.overlay.sizePercent / 100f,
        //  new UnityEngine.Color(0, 0, 0, .9f), //game.overlay.backgroundColor.ToUnity(),
        //  1f, // fadeInEnd
        //  5f, // fadeOutStart
        //  6f, // fadeOutEnd,

        //  "My brother was an explorer...", // game.overlay.overlayText,
        //  new UnityEngine.Color(0, 1, 1, 1), // game.overlay.overlayTextColor.ToUnity(),
        //  1f, // textFadeInStartS
        //  2f, // textFadeInEndS
        //  4f, // textFadeOutStartS
        //  5f, // textFadeOutEndS
        //  true,
        //  false,
        //  new List<string>());//buttonTexts);

        clock.SetTimeSpeedMultiplier(0f);
      } else if (!game.overlay.Exists()) {
        overlayPanelView.Cancel();
      }
    }

    private void OverlayClosed(int buttonIndex) {
      clock.SetTimeSpeedMultiplier(1f);
      ss.RequestOverlayAction(game.id, buttonIndex);
    }
  }
}
