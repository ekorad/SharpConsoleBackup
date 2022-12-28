using SharpConsole.Util;

AnsiEscapeSequence fgColorRed = new(AnsiCommand.SelectGraphicRendition, 38, 5, 1);
AnsiEscapeSequence resetAllSGR = new(AnsiCommand.SelectGraphicRendition, 0);
Console.WriteLine(fgColorRed + "Hello world in red!");
Console.WriteLine(resetAllSGR + "Hello world in white!");