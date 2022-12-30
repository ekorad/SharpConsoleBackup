namespace SharpConsole.SGR;

/// <summary>
/// Represents an ANSI byte code which specifies whether a color is a
/// <b>foreground</b> or <b>background</b> color.
/// </summary>
public enum SGRColorCategory
{
    /// <summary>
    /// Specifies a foreground color.
    /// </summary>
    ForegroundColor = 38,
    /// <summary>
    /// Specifies a background color.
    /// </summary>
    BackgroundColor = 48
}
