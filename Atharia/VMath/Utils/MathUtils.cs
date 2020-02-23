using System;
using System.Collections.Generic;

namespace Geomancer.Model {
  public struct Triangle {
    public readonly List<Vec2> corners;
    public Triangle(List<Vec2> corners) {
      this.corners = corners;
    }
  }

  public struct Polygon {
    public readonly List<Vec2> corners;
    public Polygon(List<Vec2> corners) {
      this.corners = corners;
    }
  }

  public struct Ray {
    public readonly Vec2 start, direction;
    public Ray(Vec2 start, Vec2 direction) {
      this.start = start;
      this.direction = direction;
    }
  }

  // May look like a Ray, but it goes both directions.
  public struct Line {
    public readonly Vec2 start, direction;
    public Line(Vec2 start, Vec2 direction) {
      this.start = start;
      this.direction = direction;
    }
  }

  public struct Segment {
    public readonly Vec2 start, end;
    public Segment(Vec2 start, Vec2 end) {
      this.start = start;
      this.end = end;
    }
  }

  public class MathUtils {
    public static bool Intersects(Polygon polygon, Line line) {
      // Note that we're only checking N-1 sides, not all N sides. That's because a line must
      // intersect with an even number of sides.
      for (int i = 0; i < polygon.corners.Count - 1; i++) {
        Vec2 intersectionPoint = new Vec2(0, 0);
        Segment side = new Segment(polygon.corners[i], polygon.corners[i + 1]);
        if (Intersects(line, side, out intersectionPoint)) {
          return true;
        }
      }
      return false;
    }
    public static bool Intersects(Polygon polygon, Segment segment) {
      for (int i = 0; i < polygon.corners.Count; i++) {
        Vec2 intersectionPoint = new Vec2(0, 0);
        Segment side =
            new Segment(polygon.corners[i], polygon.corners[(i + 1) % polygon.corners.Count]);
        if (Intersects(segment, side, out intersectionPoint)) {
          return true;
        }
      }
      return false;
    }
    public static bool Intersects(Polygon polygon, Ray ray) {
      for (int i = 0; i < polygon.corners.Count; i++) {
        Vec2 intersectionPoint = new Vec2(0, 0);
        Segment side =
            new Segment(polygon.corners[i], polygon.corners[(i + 1) % polygon.corners.Count]);
        if (Intersects(ray, side, out intersectionPoint)) {
          return true;
        }
      }
      return false;
    }


    static bool Intersects(
        Line line,
        Segment segment,
        out Vec2 intersectionPoint) {
      bool linesIntersect = false;
      float lineT = 0;
      float segmentT = 0;
      Vec2 intersection = new Vec2(0, 0);
      Vec2 closeP1 = new Vec2(0, 0);
      Vec2 closeP2 = new Vec2(0, 0);
      FindIntersection(
          line.start,
          line.start.plus(line.direction),
          segment.start,
          segment.end,
          out linesIntersect,
          out lineT,
          out segmentT,
          out intersection,
          out closeP1,
          out closeP2);
      intersectionPoint = intersection;
      return linesIntersect && (segmentT >= 0 && segmentT <= 1);
    }

    static bool Intersects(
        Ray ray,
        Segment segment,
        out Vec2 intersectionPoint) {
      bool linesIntersect = false;
      float rayT = 0;
      float segmentT = 0;
      Vec2 intersection = new Vec2(0, 0);
      Vec2 closeP1 = new Vec2(0, 0);
      Vec2 closeP2 = new Vec2(0, 0);
      FindIntersection(
          ray.start,
          ray.start.plus(ray.direction),
          segment.start,
          segment.end,
          out linesIntersect,
          out rayT,
          out segmentT,
          out intersection,
          out closeP1,
          out closeP2);
      intersectionPoint = intersection;
      return linesIntersect && rayT >= 0 && (segmentT >= 0 && segmentT <= 1);
    }

    static bool Intersects(
        Segment segmentA,
        Segment segmentB,
        out Vec2 intersectionPoint) {
      bool linesIntersect = false;
      float segmentAT = 0;
      float segmentBT = 0;
      Vec2 intersection = new Vec2(0, 0);
      Vec2 closeP1 = new Vec2(0, 0);
      Vec2 closeP2 = new Vec2(0, 0);
      FindIntersection(
          segmentA.start,
          segmentA.end,
          segmentB.start,
          segmentB.end,
          out linesIntersect,
          out segmentAT,
          out segmentBT,
          out intersection,
          out closeP1,
          out closeP2);
      intersectionPoint = intersection;
      return linesIntersect &&
          (segmentAT >= 0 && segmentAT <= 1) &&
          (segmentBT >= 0 && segmentBT <= 1);
    }

    // Find the point of intersection between the lines p1 --> p2 and p3 --> p4.
    // From http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/
    // Could probably optimize this into more specialized methods...
    // The segments intersect if line1t and line2t are between 0 and 1.
    private static void FindIntersection(
        Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4,
        out bool lines_intersect,
        out float line1t,
        out float line2t,
        out Vec2 intersection,
        out Vec2 close_p1,
        out Vec2 close_p2) {
      // Get the segments' parameters.
      float dx12 = p2.x - p1.x;
      float dy12 = p2.y - p1.y;
      float dx34 = p4.x - p3.x;
      float dy34 = p4.y - p3.y;

      // Solve for t1 and t2
      float denominator = (dy12 * dx34 - dx12 * dy34);

      float t1 = ((p1.x - p3.x) * dy34 + (p3.y - p1.y) * dx34) / denominator;
      if (float.IsInfinity(t1)) {
        // The lines are parallel (or close enough to it).
        lines_intersect = false;
        line1t = 0;
        line2t = 0;
        intersection = new Vec2(float.NaN, float.NaN);
        close_p1 = new Vec2(float.NaN, float.NaN);
        close_p2 = new Vec2(float.NaN, float.NaN);
        return;
      }
      lines_intersect = true;

      float t2 = ((p3.x - p1.x) * dy12 + (p1.y - p3.y) * dx12) / -denominator;

      // Find the point of intersection.
      intersection = new Vec2(p1.x + dx12 * t1, p1.y + dy12 * t1);

      line1t = t1;
      line2t = t2;

      // Find the closest points on the segments.
      if (t1 < 0) {
        t1 = 0;
      } else if (t1 > 1) {
        t1 = 1;
      }

      if (t2 < 0) {
        t2 = 0;
      } else if (t2 > 1) {
        t2 = 1;
      }

      close_p1 = new Vec2(p1.x + dx12 * t1, p1.y + dy12 * t1);
      close_p2 = new Vec2(p3.x + dx34 * t2, p3.y + dy34 * t2);
    }

    //static bool Intersects(
    //    Line lineA,
    //    Line lineB,
    //    float epsilon,
    //    out Vec2 intersectionPoint) {
    //  // This is lineB.direction.x / lineA.direction.x =~= lineB.direction.y / lineA.direction.y
    //  // rearranged.
    //  if (Math.Abs(lineB.direction.x * lineA.direction.y - lineB.direction.y * lineA.direction.x) <
    //      epsilon) {
    //    intersectionPoint = new Vec2(0, 0);
    //    return false;
    //  }

    //  float lineBYIntercept = 

    //  y = m1x + b1
    //  y = m2x + b2
    //  x = (b2 - b1) / (m1 - m2)
    //}
  }
}
