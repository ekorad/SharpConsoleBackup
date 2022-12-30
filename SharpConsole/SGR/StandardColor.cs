namespace SharpConsole.SGR;

/// <summary>
/// Represents an ANSI byte code which specifies one of the 16 standard colors.
/// </summary>
/// <remarks>
/// <para>
/// For more information, see
/// <see href="https://en.wikipedia.org/wiki/ANSI_escape_code">
/// ANSI Escape Code</see>.
/// </para>
/// </remarks>
public enum StandardColor : byte
{
    /// <summary>
    /// Specifies the black color.
    /// </summary>
    Black = 0,
    /// <summary>
    /// Specifies the red color.
    /// </summary>
    Red,
    /// <summary>
    /// Specifies the green color.
    /// </summary>
    Green,
    /// <summary>
    /// Specifies the yellow color.
    /// </summary>
    Yellow,
    /// <summary>
    /// Specifies the blue color.
    /// </summary>
    Blue,
    /// <summary>
    /// Specifies the magenta color.
    /// </summary>
    Magenta,
    /// <summary>
    /// Specifies the cyan color.
    /// </summary>
    Cyan,
    /// <summary>
    /// Specifies the light gray (white) color.
    /// </summary>
    LightGray,
    /// <summary>
    /// Specifies the dark gray (bright black) color.
    /// </summary>
    DarkGray,
    /// <summary>
    /// Specifies the bright red color.
    /// </summary>
    BrightRed,
    /// <summary>
    /// Specifies the bright green color.
    /// </summary>
    BrightGreen,
    /// <summary>
    /// Specifies the bright yellow color.
    /// </summary>
    BrightYellow,
    /// <summary>
    /// Specifies the bright blue color.
    /// </summary>
    BrightBlue,
    /// <summary>
    /// Specifies the bright magenta color.
    /// </summary>
    BrightMagenta,
    /// <summary>
    /// Specifies the bright cyan color.
    /// </summary>
    BrightCyan,
    /// <summary>
    /// Specifies the white (bright white) color.
    /// </summary>
    White
}
