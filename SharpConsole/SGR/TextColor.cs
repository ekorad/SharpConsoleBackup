using SharpConsole.Util;

namespace SharpConsole.SGR;

/// <summary>
/// Represents the base class for text color.
/// </summary>
/// <remarks>
/// Since this class inherits an <see cref="AnsiArgumentSequence"/>, it should
/// be used as part of an <see cref="AnsiControlSequence"/> for changing the
/// color of text.
/// <para>
/// The actual implementations of the text colors are
/// <see cref="ForegroundColor{TColor}"/> and
/// <see cref="BackgroundColor{TColor}"/>.
/// </para>
/// <para>
/// The actual type of the color (indexed or RGB) is specified through
/// <typeparamref name="TColor"/>.
/// </para>
/// </remarks>
/// <typeparam name="TColor">The color type</typeparam>
public abstract class TextColor<TColor> : AnsiArgumentSequence
    where TColor : AnsiArgumentSequence
{
    /// <summary>
    /// The color of the text.
    /// </summary>
    public TColor Color { get; }

    /// <summary>
    /// Construct a text color object.
    /// </summary>
    /// <param name="colorCategory">
    /// Specifies whether the color is a foreground or background color
    /// </param>
    /// <param name="color">The color</param>
    public TextColor(SGRColorCategory colorCategory, TColor color)
        : base(color.Arguments.Prepend((byte)colorCategory).ToArray())
        => Color = color;
}
