using SharpConsole.SGR;
using SharpConsole.Util;

ForegroundColor<RGBColor> fgColor = new(new(60, 60, 60));
BackgroundColor<IndexedColor> bgColor = new(new(StandardColor.White));
AnsiControlSequence colorSeq
    = new(AnsiCommand.SelectGraphicRendition, fgColor, bgColor);
AnsiControlSequence resetSGR = new(AnsiCommand.SelectGraphicRendition, 0);

Console.WriteLine(colorSeq + "Hello, world!" + resetSGR);