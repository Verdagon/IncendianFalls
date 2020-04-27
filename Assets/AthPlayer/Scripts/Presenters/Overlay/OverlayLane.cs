using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.AthPlayer.Scripts.Presenters.Overlay {
  class OverlayLane {

    /*
     * the purpose of lanes is to have the panels move together. its to coordinate so that
     * when the one above fades out and away, the one below will move up. we gotta maintain a
     * linked list or something basically. gotta use the animation system or something to move
     * them into the right place.
     * we cant have each panel just know about the one above because the one above might disappear
     * and then we have a new one above. no, we need to maintain a centralized list.
     * it will notify the panels when to move up.
     * or actually, do panels really need to know when to move up? maybe theyre just dumb contents
     * whose Y is moved out from under them.
     * maybe lanes are the ones with the 
     * 
     * 
     * we can show a tiny little preview thing at the bottom, faded out, waiting to scroll all the way in.
     * we run into a certain problem here tho:
     *  ________________
     * /  ____________  \
     * | /            \ |
     * | | you b idot | |
     * | \____________/ |
     * |  ____________  |
     * | /            \ |
     * | |  get rekt  | |
     * | \____________/ |
     * \________________/
     *   /            \
     *   |   scrub    |
     *   \____________/
     * 
     * where even if we make transparent the last one, it doesnt indicate to the player that anythings
     * waiting. so, maybe we'll want to add another row at the very end, just for viewing upcomings.
     * or, IOW, reserve another row at the end, just for viewing upcoming ones.
     *  ________________
     * /  ____________  \
     * | /            \ |
     * | | you b idot | |
     * | \____________/ |
     * |  ____________  |
     * | /            \ |
     * | |  get rekt  | |
     * | \____________/ |
     * |  ____________  |
     * \_/____________\_/
     *   |   scrub    |
     *   \____________/
     * 
     * how do we handle floating dialog boxes? itd be nice to have multiple onscreen at the same time.
     * maybe in that case we can make one-off lanes which are just for cinematics?
     */
  }
}
