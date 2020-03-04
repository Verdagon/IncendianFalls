using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AthPlayer {
  class OverlayPresenter {
    SlowableTimerClock timer;
    SlowableTimerClock cinematicTimer;
    ISuperstructure ss;
    Game game;
    OverlayPanelView overlayPanelView;

    private List<string> triggerNames = new List<string>();

    public OverlayPresenter(
      SlowableTimerClock timer,
        SlowableTimerClock cinematicTimer,
        ISuperstructure ss,
        Game game,
        OverlayPanelView overlayPanelView) {
      this.timer = timer;
      this.cinematicTimer = cinematicTimer;
      this.ss = ss;
      this.game = game;
      this.overlayPanelView = overlayPanelView;

      overlayPanelView.OverlayClosed += OverlayClosed;
    }

    public void ShowOverlay(ShowOverlayEvent overlay) {
      triggerNames = new List<string>();
      var buttonTexts = new List<string>();
      foreach (var button in overlay.buttons) {
        buttonTexts.Add(button.label);
        triggerNames.Add(button.triggerName);
      }
      triggerNames.Add(overlay.automaticActionTriggerName);

      overlayPanelView.Init(
        cinematicTimer,

        overlay.sizePercent / 100f,
        overlay.backgroundColor.ToUnity(),
        overlay.fadeInEndMs,
        overlay.fadeOutStartMs,
        overlay.fadeOutEndMs,

        overlay.text,
        overlay.textColor.ToUnity(),
        overlay.textFadeInStartMs,
        overlay.textFadeInEndMs,
        overlay.textFadeOutStartMs,
        overlay.textFadeOutEndMs,
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

      timer.SetTimeSpeedMultiplier(0f);
    }

    private void OverlayClosed(int buttonIndex) {
      timer.SetTimeSpeedMultiplier(1f);
      ss.RequestTrigger(game.id, triggerNames[buttonIndex]);
    }
  }
}
