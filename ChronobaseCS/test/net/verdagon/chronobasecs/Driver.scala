package net.verdagon.chronobasecs

import java.io.{File, PrintWriter}

import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions
import net.verdagon.chronobasecs.generator.chronobase.{Generator, ChronobaseOptions}
import net.verdagon.chronobasecs.parser.VCSParser

import scala.io.Source

object Driver {
  def main(args: Array[String]): Unit = {

    val module = "ravaarcana/ravaarcana.vmod";

    var lines = Source.fromResource(module).getLines().toList;

    var outputPath = lines.head;
    assert(outputPath.startsWith("output: "));
    outputPath = outputPath.drop("output: ".length);
    lines = lines.tail;

    var ns = lines.head;
    assert(ns.startsWith("namespace: "));
    ns = ns.drop("namespace: ".length);
    lines = lines.tail;

    val files = lines;

    val code =
      files
        .flatMap(file => Source.fromResource(file).getLines())
        .mkString("\n")
        .replaceAll("//.*", "")

    var ssP =
      VCSParser.parse(VCSParser.superstructure, code.toCharArray) match {
        case VCSParser.NoSuccess(msg, input) => throw new RuntimeException(msg);
        case VCSParser.Success(expr, rest) => {
          if (!rest.atEnd) {
            throw new RuntimeException(rest.pos.longString)
          }
          expr
        }
      }

    var ssS = Compiler.compile(ssP)

    val map = Generator.generateSuperstructure(ChronobaseOptions(false), ssS)

    map.foreach({ case (className, definition) =>
      val pathAndName = outputPath + "/" + className + ".cs"
//      println(pathAndName + ":\n" + definition + "\n")
      println(pathAndName)
      val writer = new PrintWriter(new File(pathAndName))
      writer.write(
        "using System;\n" +
        "using System.Collections;\n\n" +
        "using System.Collections.Generic;\n\n" +
        "namespace " + ns + " {\n" +
        definition +
        "\n}\n")
      writer.close()
    })
  }
}
