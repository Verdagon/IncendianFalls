using System.IO;

namespace IncendianFalls {
  public class SuperstructureLogger {
    long date;
    public SuperstructureLogger(long date) {
      this.date = date;
    }
    void Write(string str) {
      // Write to disk
      StreamWriter writer = new StreamWriter("Log" + date + ".sslog", true);
      writer.Write(str + "\n");

      //// Read
      //StreamReader reader = new StreamReader("MyPath.txt");
      //string lineA = reader.ReadLine();
      //string[] splitA = lineA.Split(',');
      //scoreA = int.Parse(splitA[1]);

      //string lineB = reader.ReadLine();
      //string[] splitB = lineB.Split(',');
      //scoreB = int.Parse(splitB[1]);
    }
  }
}