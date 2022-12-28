namespace SharpConsole.Util;

/// <summary>
/// Provides definitions for the most common ANSI commands.
/// </summary>
/// <remarks>
/// The ANSI command code resides at the end of an ANSI sequence.
/// <para>See <see href="https://en.wikipedia.org/wiki/ANSI_escape_code">
/// ANSI Escape Code</see> for further details.</para>
/// </remarks>
public enum AnsiCommand
{
    /// <summary>
    /// Move the cursor up a number of cells.
    /// </summary>
    CursorUp = 'A',
    /// <summary>
    /// Move the cursor down a number of cells.
    /// </summary>
    CursorDown = 'B',
    /// <summary>
    /// Move the cursor forwards a number of cells.
    /// </summary>
    CursorForward = 'C',
    /// <summary>
    /// Move the cursor backwards a number of cells.
    /// </summary>
    CursorBackward = 'D',
    /// <summary>
    /// Move the cursor a number of lines down, at the beginning of the line.
    /// </summary>
    CursorNextLine = 'E',
    /// <summary>
    /// Move the cursor a number of lines up, at the beginning of the line.
    /// </summary>
    CursorPreviousLine = 'F',
    /// <summary>
    /// Move the cursor to a specific position.
    /// </summary>
    CursorPosition = 'H',
    /// <summary>
    /// Clear part of the screen.
    /// </summary>
    ClearScreen = 'J',
    /// <summary>
    /// Scroll the whole page up by a number of lines.
    /// </summary>
    ScrollUp = 'S',
    /// <summary>
    /// Scroll the whole page down by a number of lines.
    /// </summary>
    ScrollDown = 'T',
    /// <summary>
    /// Set the colors and style of the text.
    /// </summary>
    SelectGraphicRendition = 'm'
}
