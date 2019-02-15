using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class DungeonTerrainGenerator {
    struct Room {
      public readonly AbsRowCol origin;
      public readonly SizeRowCol size;
      public Room(AbsRowCol origin, SizeRowCol size) {
        this.origin = origin;
        this.size = size;
      }
    }

    struct AbsRowCol : IComparable<AbsRowCol> {
      public readonly int row;
      public readonly int col;
      public AbsRowCol(int row, int col) {
        this.row = row;
        this.col = col;
      }
      public override string ToString() {
        return "RowCol(" + row + ", " + col + ")";
      }
      public override int GetHashCode() {
        return 1343 * 1337 * row + col;
      }
      public override bool Equals(object obj) {
        return obj is AbsRowCol && ((AbsRowCol)obj).row == row && ((AbsRowCol)obj).col == col;
      }

      public int CompareTo(AbsRowCol other) {
        if (row != other.row)
          return row - other.row;
        if (col != other.col)
          return col - other.col;
        return 0;
      }
    }

    struct SizeRowCol {
      public readonly int row;
      public readonly int col;
      public SizeRowCol(int row, int col) {
        if (row < 0 || col < 0) {
          throw new Exception("wat");
        }
        this.row = row;
        this.col = col;
      }
      public override string ToString() {
        return "RowCol(" + row + ", " + col + ")";
      }
      public override int GetHashCode() {
        return 1343 * 1337 * row + col;
      }
      public override bool Equals(object obj) {
        return obj is SizeRowCol && ((SizeRowCol)obj).row == row && ((SizeRowCol)obj).col == col;
      }
    }

    static bool IsRectangleAvailable(int[,] canvas, AbsRowCol origin, SizeRowCol size) {
      if (origin.row < 0) {
        return false;
      }
      if (origin.col < 0) {
        return false;
      }
      if (origin.row + size.row >= canvas.GetLength(0)) {
        return false;
      }
      if (origin.col + size.col >= canvas.GetLength(1)) {
        return false;
      }
      for (int rowI = origin.row; rowI < origin.row + size.row; rowI++) {
        for (int colI = origin.col; colI < origin.col + size.col; colI++) {
          if (canvas[rowI, colI] >= 0) {
            return false;
          }
        }
      }
      return true;
    }

    public static Terrain Generate(Root root, int width, int height, Rand rand) {
      Pattern pattern = SquarePattern.MakeSquarePattern();

      float elevationStepHeight = .2f;

      var roomByNumber = new SortedDictionary<int, Room>();
      var borderLocations = new SortedSet<AbsRowCol>();
      var unusedLocations = new SortedSet<AbsRowCol>();
      int nextRoomNumber = 0;

      // -1 means unused
      var canvas = new int[height, width];
      for (int row = 0; row < canvas.GetLength(0); row++) {
        for (int col = 0; col < canvas.GetLength(1); col++) {
          canvas[row, col] = -1;
          unusedLocations.Add(new AbsRowCol(row, col));
        }
      }

      // 100 attempts to make rooms.
      for (int i = 0; i < 100; i++) {
        // We want a buffer empty space of 1, and then a wall space... on each side.
        // So width and height are 4 higher than you'd expect.
        // Walls count as part of the room
        int minWidth = 4 + 4;
        int minHeight = 3 + 4;
        int maxWidth = rand.Next(6, 12) + 4;
        int maxHeight = rand.Next(4, 8) + 4;

        AbsRowCol roomOrigin = SetUtils.GetRandom(rand.Next(), unusedLocations);
        //Logger.Info("Have origin " + roomOrigin);

        SizeRowCol roomSize = new SizeRowCol(0, 0);
        if (!IsRectangleAvailable(canvas, roomOrigin, roomSize)) {
          throw new Exception("wat");
        }

        bool stoppedExpandingUp = false;
        bool stoppedExpandingDown = false;
        bool stoppedExpandingLeft = false;
        bool stoppedExpandingRight = false;

        for (int emergencyBrakes = 0; !stoppedExpandingUp || !stoppedExpandingDown ||
             !stoppedExpandingLeft || !stoppedExpandingRight; emergencyBrakes++) {
          if (emergencyBrakes >= canvas.GetLength(0) + canvas.GetLength(1)) {
            throw new Exception("wat");
          }
          if (!stoppedExpandingUp) {
            if (IsRectangleAvailable(canvas, new AbsRowCol(roomOrigin.row - 1, roomOrigin.col), new SizeRowCol(1, roomSize.col))) {
              roomOrigin = new AbsRowCol(roomOrigin.row - 1, roomOrigin.col);
              roomSize = new SizeRowCol(roomSize.row + 1, roomSize.col);
              // Now that we've expanded up, check to see if we've hit the max height.
              if (roomSize.row >= maxHeight) {
                stoppedExpandingUp = true;
                stoppedExpandingDown = true;
              }
            } else {
              stoppedExpandingUp = true;
            }
          }

          if (!stoppedExpandingDown) {
            if (IsRectangleAvailable(canvas, new AbsRowCol(roomOrigin.row + roomSize.row, roomOrigin.col), new SizeRowCol(1, roomSize.col))) {
              // roomOrigin unchanged
              roomSize = new SizeRowCol(roomSize.row + 1, roomSize.col);
              // Now that we've expanded down, check to see if we've hit the max height.
              if (roomSize.row >= maxHeight) {
                stoppedExpandingUp = true;
                stoppedExpandingDown = true;
              }
            } else {
              stoppedExpandingDown = true;
            }
          }

          if (!stoppedExpandingLeft) {
            if (IsRectangleAvailable(canvas, new AbsRowCol(roomOrigin.row, roomOrigin.col - 1), new SizeRowCol(roomSize.row, 1))) {
              roomOrigin = new AbsRowCol(roomOrigin.row, roomOrigin.col - 1);
              roomSize = new SizeRowCol(roomSize.row, roomSize.col + 1);
              // Now that we've expanded left, check to see if we've hit the max width.
              if (roomSize.col >= maxWidth) {
                stoppedExpandingLeft = true;
                stoppedExpandingRight = true;
              }
            } else {
              stoppedExpandingLeft = true;
            }
          }

          if (!stoppedExpandingRight) {
            if (IsRectangleAvailable(canvas, new AbsRowCol(roomOrigin.row, roomOrigin.col + roomSize.col), new SizeRowCol(roomSize.row, 1))) {
              // roomOrigin unchanged
              roomSize = new SizeRowCol(roomSize.row, roomSize.col + 1);
              // Now that we've expanded right, check to see if we've hit the max width.
              if (roomSize.col >= maxWidth) {
                stoppedExpandingLeft = true;
                stoppedExpandingRight = true;
              }
            } else {
              stoppedExpandingRight = true;
            }
          }
        }

        if (roomSize.row < minHeight || roomSize.col < minWidth) {
          continue;
        }

        int roomNumber = nextRoomNumber++;
        var roomSpaces = new SortedSet<AbsRowCol>();
        for (int rowI = roomOrigin.row; rowI < roomOrigin.row + roomSize.row; rowI++) {
          for (int colI = roomOrigin.col; colI < roomOrigin.col + roomSize.col; colI++) {
            canvas[rowI, colI] = roomNumber;
            roomSpaces.Add(new AbsRowCol(rowI, colI));
          }
        }
        SetUtils.RemoveAll(unusedLocations, roomSpaces);
        roomByNumber.Add(
            roomNumber,
            new Room(
                new AbsRowCol(roomOrigin.row + 2, roomOrigin.col + 2),
                new SizeRowCol(roomSize.row - 4, roomSize.col - 4)));

        //if (roomByNumber.Count >= 5) { // remove me
        //  break;
        //}
      }

      // The rectangles in canvas are bigger than the rectangles described by roomByNumber,
      // because the canvas included the buffers. Let's remake the canvas according to
      // roomByNumber.
      for (int row = 0; row < canvas.GetLength(0); row++) {
        for (int col = 0; col < canvas.GetLength(1); col++) {
          canvas[row, col] = -1;
        }
      }
      foreach (var numberAndRoom in roomByNumber) {
        var room = numberAndRoom.Value;
        //Logger.Info("Room " + numberAndRoom.Key + ": " + room.origin + " and " + room.size);
        for (int rowI = room.origin.row; rowI < room.origin.row + room.size.row; rowI++) {
          for (int colI = room.origin.col; colI < room.origin.col + room.size.col; colI++) {
            canvas[rowI, colI] = numberAndRoom.Key;
          }
        }
      }

      // Now that we have a bunch of rooms, let's connect them.

      ConnectRooms(canvas, rand, roomByNumber);

      var tiles = root.EffectTerrainTileByLocationMutMapCreate();

      foreach (var room in roomByNumber.Values) {
        for (int rowI = room.origin.row; rowI < room.origin.row + room.size.row; rowI++) {
          for (int colI = room.origin.col; colI < room.origin.col + room.size.col; colI++) {
            //Logger.Info("Adding " + rowI + " " + colI);
            var tile = root.EffectTerrainTileCreate(1, true, "stone");
            tiles.Add(new Location(colI, rowI, 0), tile);
          }
        }
      }

      var allTiles = new SortedSet<Location>(tiles.Keys);
      var allAdjacent = pattern.GetAdjacentLocations(allTiles, true);
      SetUtils.RemoveAll(allAdjacent, allTiles);
      foreach (var borderLocation in allAdjacent) {
        var tile = root.EffectTerrainTileCreate(2, false, "stone");
        tiles.Add(borderLocation, tile);
      }

      var terrain = root.EffectTerrainCreate(pattern, elevationStepHeight, tiles);
      return terrain;
    }



    static void ConnectRooms(int[,] canvas, Rand rand, SortedDictionary<int, Room> roomByNumber) {
      // This function will be adding the corridors to roomByNumber.

      // I would just use integers but C# has no typedefs >:(
      var regions = new SortedSet<string>();

      var regionByRoomNumber = new SortedDictionary<int, String>();
      var roomNumbersByRegion = new SortedDictionary<String, SortedSet<int>>();

      foreach (var roomNumberAndRoom in roomByNumber) {
        int roomNumber = roomNumberAndRoom.Key;
        String region = "region" + roomNumber;
        regionByRoomNumber.Add(roomNumber, region);
        var roomNumbersInRegion = new SortedSet<int>();
        roomNumbersInRegion.Add(roomNumber);
        roomNumbersByRegion.Add(region, roomNumbersInRegion);
        regions.Add(region);
        //Logger.Info("Made region " + region);
      }

      for (int ebrakesA = 0, ebrakesAMax = roomByNumber.Count + 5; ; ebrakesA++) {
        if (ebrakesA > ebrakesAMax) {
          //UnityEngine.Debug.LogError("wat");
          throw new Exception("wat");
          //return;
        }

        //Logger.Info("Starting iteration!");
        //foreach (var roomNumberAndRegion in regionByRoomNumber) {
        //  Logger.Info(roomNumberAndRegion.Key + " is in " + roomNumberAndRegion.Value);
        //}
        //foreach (var regionAndRoomNumbers in roomNumbersByRegion) {
        //  foreach (var roomNumber in regionAndRoomNumbers.Value) {
        //    Logger.Info(regionAndRoomNumbers.Key + " has " + roomNumber);
        //  }
        //}

        var distinctRegions = new SortedSet<String>(regionByRoomNumber.Values);
        //Logger.Info(distinctRegions.Count + " distinct regions!");
        if (distinctRegions.Count < 2) {
          break;
        }
        var twoRegions = SetUtils.GetFirstN(distinctRegions, 2);
        String regionA = twoRegions[0];
        String regionB = twoRegions[1];
        //Logger.Info("Will aim to connect regions " + regionA + " and " + regionB);

        int regionARoomNumber = SetUtils.GetRandom(rand.Next(), roomNumbersByRegion[regionA]);
        var regionARoom = roomByNumber[regionARoomNumber];
        var regionALocation =
            new AbsRowCol(
                regionARoom.origin.row + rand.Next(0, regionARoom.size.row - 1),
                regionARoom.origin.col + rand.Next(0, regionARoom.size.col - 1));

        int regionBRoomNumber = SetUtils.GetRandom(rand.Next(), roomNumbersByRegion[regionB]);
        var regionBRoom = roomByNumber[regionBRoomNumber];
        var regionBLocation =
            new AbsRowCol(
                regionBRoom.origin.row + rand.Next(0, regionBRoom.size.row - 1),
                regionBRoom.origin.col + rand.Next(0, regionBRoom.size.col - 1));

        // Now lets drive from regionALocation to regionBLocation, and see what happens on the
        // way there.
        List<AbsRowCol> overallPath = new List<AbsRowCol>();
        List<AbsRowCol> horizontalPath = new List<AbsRowCol>();
        List<AbsRowCol> verticalPath = new List<AbsRowCol>();
        AbsRowCol currentLocation = regionALocation;
        //Logger.Info("Going from region " + regionA + " to " + regionB);
        //Logger.Info("Going from loc " + regionALocation + " to " + regionBLocation);

        bool stillGoingHorizontal = true;
        for (int emergencyBrakes = 0; ; emergencyBrakes++) {
          if (emergencyBrakes > canvas.GetLength(0) + canvas.GetLength(1) || regionALocation.Equals(regionBLocation)) {
            throw new Exception("wat");
          }
          if (stillGoingHorizontal && currentLocation.col == regionBLocation.col) {
            stillGoingHorizontal = false;
          }
          if (stillGoingHorizontal) {
            currentLocation =
                new AbsRowCol(
                    currentLocation.row,
                    currentLocation.col + Math.Sign(regionBLocation.col - currentLocation.col));
          } else {
            currentLocation =
                new AbsRowCol(
                    currentLocation.row + Math.Sign(regionBLocation.row - currentLocation.row),
                    currentLocation.col);
          }
          if (currentLocation.row < 0 || currentLocation.col < 0 || currentLocation.row >= canvas.GetLength(0) || currentLocation.col >= canvas.GetLength(1)) {
            throw new Exception("wat");
          }

          var adjacentLocations = new AbsRowCol[] {
            new AbsRowCol(currentLocation.row - 1, currentLocation.col),
            new AbsRowCol(currentLocation.row + 1, currentLocation.col),
            new AbsRowCol(currentLocation.row, currentLocation.col - 1),
            new AbsRowCol(currentLocation.row, currentLocation.col + 1)
          };
          var adjacentRegions = new SortedSet<string>();
          foreach (var adjacentLocation in adjacentLocations) {
            if (adjacentLocation.row >= 0 && adjacentLocation.col >= 0 &&
                adjacentLocation.row < canvas.GetLength(0) && adjacentLocation.col < canvas.GetLength(1)) {
              int roomNumber = canvas[adjacentLocation.row, adjacentLocation.col];
              if (roomNumber != -1) {
                String region = regionByRoomNumber[roomNumber];
                adjacentRegions.Add(region);
              }
            }
          }

          int currentRoomNumber = canvas[currentLocation.row, currentLocation.col];
          if (currentRoomNumber != -1 && regionByRoomNumber[currentRoomNumber] == regionA) {
            // We're touching something from our own region.
            // Keep going, but restart the path here.
            //Logger.Info("Restarting from here " + currentLocation + " because it's from the original region");
            overallPath.Clear();
            horizontalPath.Clear();
            verticalPath.Clear();
          } else {
            overallPath.Add(currentLocation);
            if (stillGoingHorizontal) {
              //Logger.Info("Adding " + currentLocation + " to horizontal path");
              horizontalPath.Add(currentLocation);
            } else {
              //Logger.Info("Adding " + currentLocation + " to vertical path");
              verticalPath.Add(currentLocation);
            }
          }
          if (adjacentRegions.Count == 0) {
            // It means we're in open space, keep going.
          } else if (adjacentRegions.Count == 1 && SetUtils.GetFirst(adjacentRegions) == regionA) {
            // Then we're only next to our original region. Keep going.
          } else if (adjacentRegions.Count > 1 || SetUtils.GetFirst(adjacentRegions) != regionA) {
            // We're touching another region! It's probably regionB, but isn't necessarily...
            // we could have just come across a random other region. Either way, we hit something,
            // so we stop now.
            break;
          } else {
            throw new Exception("wot");
          }
        }
        //Logger.Info("Path size " + overallPath.Count);
        if (overallPath.Count == 0) {
          //UnityEngine.Debug.LogError("wat");
          throw new Exception("wat");
          //return;
        }

        String combinedRegion = "region" + regions.Count;
        regions.Add(combinedRegion);

        var roomNumbersInCombinedRegion = new SortedSet<int>();
        if (horizontalPath.Count > 0) {
          AbsRowCol startLocation = horizontalPath[0];
          AbsRowCol endLocation = horizontalPath[horizontalPath.Count - 1];
          var origin =
              new AbsRowCol(
                  startLocation.row,
                  Math.Min(startLocation.col, endLocation.col));
          var size =
              new SizeRowCol(
                  1,
                  Math.Abs(startLocation.col - endLocation.col) + 1);

          int newRoomNumber = roomByNumber.Count;
          roomByNumber.Add(newRoomNumber, new Room(origin, size));
          foreach (var pathLocation in horizontalPath) {
            canvas[pathLocation.row, pathLocation.col] = newRoomNumber;
          }
          regionByRoomNumber.Add(newRoomNumber, combinedRegion);
          roomNumbersInCombinedRegion.Add(newRoomNumber);
        }
        if (verticalPath.Count > 0) {
          AbsRowCol startLocation = verticalPath[0];
          AbsRowCol endLocation = verticalPath[verticalPath.Count - 1];
          var origin =
              new AbsRowCol(
                  Math.Min(startLocation.row, endLocation.row),
                  startLocation.col);
          var size =
              new SizeRowCol(
                  Math.Abs(startLocation.row - endLocation.row) + 1,
                  1);

          int newRoomNumber = roomByNumber.Count;
          roomByNumber.Add(newRoomNumber, new Room(origin, size));
          foreach (var pathLocation in verticalPath) {
            canvas[pathLocation.row, pathLocation.col] = newRoomNumber;
          }
          regionByRoomNumber.Add(newRoomNumber, combinedRegion);
          roomNumbersInCombinedRegion.Add(newRoomNumber);
        }


        // We'll fill in regionNumberByRoomNumber and roomNumbersByRegionNumber shortly.

        // So, now we have a path that we know connects some regions. However, it might be
        // accidentally connecting more than two! It could have grazed past another region without
        // us realizing it.
        // So now, figure out all the regions that this path touches.

        // BTW, if we ever see corridors that are connected to other corridors or rooms only
        // diagonally, it could be because in this loop we consider diagonals too, but in the
        // previous loop we only stopped if we were side-adjacent to a different region.

        var pathAdjacentLocations = new SortedSet<AbsRowCol>();
        foreach (var location in overallPath) {
          for (var rowI = Math.Max(0, location.row - 1); rowI <= Math.Min(canvas.GetLength(0), location.row + 1); rowI++) {
            for (var colI = Math.Max(0, location.col - 1); colI <= Math.Min(canvas.GetLength(1), location.col + 1); colI++) {
              pathAdjacentLocations.Add(new AbsRowCol(rowI, colI));
            }
          }
        }

        var pathAdjacentRegions = new SortedSet<String>();
        foreach (var pathAdjacentLocation in pathAdjacentLocations) {
          int roomNumber = canvas[pathAdjacentLocation.row, pathAdjacentLocation.col];
          if (roomNumber != -1) {
            String region = regionByRoomNumber[roomNumber];
            pathAdjacentRegions.Add(region);
          }
        }

        foreach (var pathAdjacentRegion in pathAdjacentRegions) {
          if (pathAdjacentRegion == combinedRegion) {
            // The new room is already part of this region
            continue;
          }
          foreach (var pathAdjacentRoomNumber in roomNumbersByRegion[pathAdjacentRegion]) {
            //Logger.Info("Overwriting " + pathAdjacentRoomNumber + "'s region to " + combinedRegion);
            regionByRoomNumber[pathAdjacentRoomNumber] = combinedRegion;
            roomNumbersInCombinedRegion.Add(pathAdjacentRoomNumber);
          }
          roomNumbersByRegion.Remove(pathAdjacentRegion);
        }

        String roomNums = "";
        foreach (var pathAdjacentRoomNumber in roomNumbersInCombinedRegion) {
          if (roomNums != "") {
            roomNums = roomNums + ", ";
          }
          roomNums = roomNums + pathAdjacentRoomNumber;
        }
        //Logger.Info("Region " + combinedRegion + " now has room numbers: " + roomNums);
        roomNumbersByRegion.Add(combinedRegion, roomNumbersInCombinedRegion);
      }
    }
  }
}
