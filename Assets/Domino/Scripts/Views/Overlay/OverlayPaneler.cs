using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class OverlayPaneler : MonoBehaviour {
    public Instantiator instantiator;

    public OverlayPanelView MakePanel(
        IClock cinematicTimer,
        int horizontalAlignmentPercent,
        int verticalAlignmentPercent,
        int widthPercent,
        int heightPercent,
        int symbolsWide,
        int symbolsHigh) {
      var spv =
        instantiator.CreateOverlayPanelView(
          gameObject,
        cinematicTimer,
          horizontalAlignmentPercent,
          verticalAlignmentPercent,
          widthPercent,
          heightPercent,
          symbolsWide,
          symbolsHigh);
      return spv;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
  }
}