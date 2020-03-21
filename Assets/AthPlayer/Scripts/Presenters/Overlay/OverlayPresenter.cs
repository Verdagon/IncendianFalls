using Atharia.Model;
using Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AthPlayer {
  // Handles the pagination and stuff for showing an overlay, and with a certain theme.
  // We could theoretically make multiple of these.
  public class OverlayPresenter {
    public class PageButton {
      public string label;
      public OverlayPanelView.OnClicked clicked;

      public PageButton(
          string label,
          OverlayPanelView.OnClicked clicked) {
        this.label = label;
        this.clicked = clicked;
      }
    }

    public delegate void OnExit();

    //SlowableTimerClock cinematicTimer;
    //OverlayPaneler overlayPaneler;
    //InputSemaphore inputSemaphore;

    IPageController pageController;
    bool isFirstInSequence;
    bool isLastInSequence;
    UnityEngine.Color textColor;
    string[] wrappedOriginalLines;
    List<PageButton> finalButtons;
    bool isPortrait;
    bool isObscuring;
    OverlayPanelView currentPageOverlayPanelView;

    public OverlayPresenter(
        SlowableTimerClock cinematicTimer,
        OverlayPaneler overlayPaneler,
        InputSemaphore inputSemaphore,
        string template,
        string role,
        bool isFirstInSequence,
        bool isLastInSequence,
        bool isObscuring,
        string unwrappedText,
        List<PageButton> buttons) {
      //this.cinematicTimer = cinematicTimer;
      //this.overlayPaneler = overlayPaneler;
      //this.inputSemaphore = inputSemaphore;

      this.isFirstInSequence = isFirstInSequence;
      this.isLastInSequence = isLastInSequence;
      this.isObscuring = isObscuring;

      switch (role) {
        case "kylin":
          textColor = new UnityEngine.Color(1, .2f, 0, 1);
          break;
        case "kylinBrother":
          textColor = new UnityEngine.Color(.5f, 1, 1, 1);
          break;
        case "narrator":
          textColor = new UnityEngine.Color(1, 1, 1, 1);
          break;
        default:
          Debug.LogWarning("Unknown role: " + role);
          textColor = new UnityEngine.Color(1, 1, 1, 1);
          break;
      }

      switch (template) {
        case "dramatic":
          pageController = new NormalPageController(overlayPaneler, cinematicTimer, inputSemaphore, true, true);
          break;
        case "aside":
          pageController = new AsidePageController(overlayPaneler, cinematicTimer, false);
          break;
        case "error":
          pageController = new AsidePageController(overlayPaneler, cinematicTimer, true);
          break;
        case "instructions":
          pageController = new InstructionsPageController(overlayPaneler, cinematicTimer);
          break;
        case "normal":
        default:
          pageController = new NormalPageController(overlayPaneler, cinematicTimer, inputSemaphore, false, false);
          break;
      }

      finalButtons = buttons;

      isPortrait = Screen.height > Screen.width;

      var (textMaxWidth, textMaxHeight) = pageController.GetPageTextMaxWidthAndHeight(isPortrait, finalButtons);
      wrappedOriginalLines = LineWrapper.Wrap(unwrappedText, textMaxWidth);

      ShowPage(0);
    }

    private void ShowPage(int pageIndex) {
      var (textMaxWidth, textMaxHeight) = pageController.GetPageTextMaxWidthAndHeight(isPortrait, finalButtons);

      List<string> pageLines = new List<string>();
      for (int i = pageIndex * textMaxHeight; i < (pageIndex + 1) * textMaxHeight && i < wrappedOriginalLines.Length; i++) {
        pageLines.Add(wrappedOriginalLines[i]);
      }

      bool isFirstPage = pageIndex == 0;

      int numPages = (wrappedOriginalLines.Length + textMaxHeight - 1) / textMaxHeight; // Rounds up
      bool isLastPage = pageIndex == numPages - 1;

      List<PageButton> buttons = finalButtons;// new List<PageButton>();
      if (isLastPage) {
        //foreach (var finalButton in finalButtons) {
        //  buttons.Add(new PageButton(finalButton.label, () => {
        //    finalButton.clicked();
        //  }));
        //}
      } else {
        buttons.Add(new PageButton("...", () => ShowPage(pageIndex + 1)));
      }

      var fadeInBackground = isFirstInSequence && isFirstPage;
      var fadeOutBackground = isLastInSequence && isLastPage;
      var callCallbackAfterFadeOut = !isObscuring;
      currentPageOverlayPanelView =
        pageController.ShowPage(
          pageLines,
          textColor,
          buttons,
          fadeInBackground,
          fadeOutBackground,
          isPortrait,
          callCallbackAfterFadeOut);
    }

    public void Close() {
      currentPageOverlayPanelView.ScheduleClose(0);
    }
  }
}
