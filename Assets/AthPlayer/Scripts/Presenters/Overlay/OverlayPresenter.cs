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

    SlowableTimerClock cinematicTimer;
    OverlayPaneler overlayPaneler;
    InputSemaphore inputSemaphore;

    IPageController pageController;
    string[] wrappedOriginalLines;
    List<PageButton> finalButtons;
    bool isPortrait;

    public OverlayPresenter(
        SlowableTimerClock cinematicTimer,
        OverlayPaneler overlayPaneler,
        InputSemaphore inputSemaphore,
        string template,
        string unwrappedText,
        List<PageButton> buttons) {
      this.cinematicTimer = cinematicTimer;
      this.overlayPaneler = overlayPaneler;
      this.inputSemaphore = inputSemaphore;

      switch (template) {
        case "normal":
          pageController = new NormalPageController(overlayPaneler, cinematicTimer, inputSemaphore);
          break;
        case "aside":
          pageController = new AsidePageController(overlayPaneler, cinematicTimer);
          break;
        default:
          pageController = new NormalPageController(overlayPaneler, cinematicTimer, inputSemaphore);
          break;
      }

      finalButtons = buttons;

      isPortrait = Screen.height > Screen.width;

      var (textMaxWidth, textMaxHeight) = pageController.GetPageTextMaxWidthAndHeight(isPortrait, finalButtons);
      wrappedOriginalLines = LineWrapper.Wrap(unwrappedText, textMaxWidth);

      ShowPage(0);
    }

    private void ShowPage(int pageIndex) {
      Debug.Log("Showing page " + pageIndex);
      var (textMaxWidth, textMaxHeight) = pageController.GetPageTextMaxWidthAndHeight(isPortrait, finalButtons);

      List<string> pageLines = new List<string>();
      for (int i = pageIndex * textMaxHeight; i < (pageIndex + 1) * textMaxHeight && i < wrappedOriginalLines.Length; i++) {
        pageLines.Add(wrappedOriginalLines[i]);
      }

      bool isFirstPage = pageIndex == 0;

      int numPages = (wrappedOriginalLines.Length + textMaxHeight - 1) / textMaxHeight; // Rounds up
      bool isLastPage = pageIndex == numPages - 1;

      Debug.Log(numPages + " " + isFirstPage + " " + isLastPage + " " + pageIndex + " " + pageLines.Count);

      List<PageButton> buttons = new List<PageButton>();
      if (isLastPage) {
        buttons = finalButtons;
      } else {
        buttons.Add(new PageButton("...", () => ShowPage(pageIndex + 1)));
      }
      pageController.ShowPage(pageLines, buttons, isFirstPage, isLastPage, isPortrait);
    }
  }
}
