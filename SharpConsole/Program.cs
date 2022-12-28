using SharpConsole.Util;
AnsiControlSequence customSequence = new(AnsiCommand.SelectGraphicRendition,
    new AnsiArgumentSequence(38, 5, 2),
    new AnsiArgumentSequence(4));
AnsiControlSequence resetAllSGR = new(AnsiCommand.SelectGraphicRendition, 0);
Console.WriteLine(customSequence + "Hello world using a custom style!");
Console.WriteLine(resetAllSGR + "Hello default world!");
