using SharpConsole.Util;

namespace SharpConsole.SGR;

/// <summary>
/// Represents a color specified through an index.
/// </summary>
/// <remarks>
/// The color index ranges from 0 to 255. Indexes 0-15 represent standard colors
/// and can also be specified using <see cref="StandardColor"/>. For more
/// information regarding the actual colors, see
/// <see href="https://en.wikipedia.org/wiki/ANSI_escape_code">
/// ANSI Escape Code</see>.
/// <para>
/// Since this class inherits an <see cref="AnsiArgumentSequence"/>, it should
/// be used as part of an <see cref="AnsiControlSequence"/> for changing the
/// color of text. An indexed color argument sequence has the following
/// structure:
/// <code><i>color category</i>;5;<i>color index</i></code>
/// The code <c>5</c> specifies that the color is an indexed color. See
/// <see cref="IndexedColorTypeCode"/> for further information.
/// </para>
/// </remarks>
public sealed class IndexedColor : AnsiArgumentSequence
{
	/// <summary>
	/// Represents the part of the ANSI argument sequence which specifies that the
	/// color is an indexed color.
	/// </summary>
	/// <remarks>
	/// This code comes second in the argument sequence after the color category
	/// code. As an example, the following sequence can be used to set the
	/// foreground color to red using an indexed color: <c>38;5;1</c>.
	/// </remarks>
	public const byte IndexedColorTypeCode = 5;

	/// <summary>
	/// Represents the index of the color, which can range from 0 to 255.
	/// </summary>
    public byte ColorIndex { get; }

	/// <summary>
	/// Construct an indexed color.
	/// </summary>
	/// <param name="colorIndex">The color index</param>
	public IndexedColor(byte colorIndex) : base(IndexedColorTypeCode, colorIndex) =>
		ColorIndex = colorIndex;

	/// <summary>
	/// Construct an indexed color.
	/// </summary>
	/// <param name="color">The color index</param>
	public IndexedColor(StandardColor color) : this((byte)color)
	{
	}

	/// <summary>
	/// Allows the explicit conversion of an <see cref="IndexedColor"/> to a
	/// <see cref="byte"/>.
	/// </summary>
	/// <param name="color">The color to be converted</param>
	public static explicit operator byte(IndexedColor color) => color.ColorIndex;

    /// <summary>
    /// Allows the explicit conversion of an <see cref="IndexedColor"/> to a
	/// <see cref="StandardColor"/>.
    /// </summary>
    /// <param name="color">the color to be converted</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///		Thrown if <paramref name="color"/> is not a valid enum value
    /// </exception>
    public static explicit operator StandardColor(IndexedColor color)
	{
		if (!Enum.IsDefined(typeof(StandardColor), color.ColorIndex))
		{
			throw new ArgumentOutOfRangeException(nameof(color));
		}
		return (StandardColor)color;
	}
}
