using System;
using Atharia.Model;

namespace IncendianFalls {
  public struct Direction : IComparable<Direction> {
    public const int NUM = 16;
    private const int RIGHT = 0;
    private const int UP = NUM / 4;
    private const int LEFT = NUM * 2 / 4;
    private const int DOWN = NUM * 3 / 4;

    public readonly int dirNum;

    public Direction(int dirNum) {
      this.dirNum = (dirNum + 2 * NUM) % NUM;
    }
    
    public static Direction operator +(Direction a, int i) => new Direction((a.dirNum + i) % NUM);
    public static Direction operator -(Direction a, int i) => new Direction((a.dirNum + NUM - (i % NUM)) % NUM);
    
    // I think we could make this super-optimized (and deterministic) if we wanted to:
    // int xOverYTimes1000 = x * 1000 / y;
    // int yOverXTimes1000 = y * 1000 / x;
    // then remember whether x was positive, y was positive, and if |x| > |y|.
    // then we only have to deal with an eighth of the world.
    // That's like 4 if-statements, which we can even simplify into multiply-with-bool
    // expressions.
    // Then reconstruct the final resulting slice number based on all those.
    public static Direction fromXY(float x, float y) {
      double radians = Math.Atan2(y, x);
      int dirNum = (int)(radians * NUM / (2 * Math.PI));
      return new Direction(dirNum);
    }
    public static Direction fromVec(Vec2 vec) {
      return fromXY(vec.x, vec.y);
    }

    // We can optimize this into a lookup table
    public Vec2 toVec() {
      double radians = dirNum * (2 * Math.PI / NUM);
      return new Vec2((float)Math.Cos(radians), (float)Math.Sin(radians));
    }

    public override int GetHashCode() { return dirNum; }
    public override bool Equals(object obj) {
      if (obj is Direction) {
        return dirNum == ((Direction) obj).dirNum;
      } else {
        return false;
      }
    }
    
    public int CompareTo(Direction that) {
      return dirNum.CompareTo(that.dirNum);
    }
  }

}