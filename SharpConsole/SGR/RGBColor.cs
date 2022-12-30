using SharpConsole.Util;
using System.Xml.Schema;

namespace SharpConsole.SGR;

/// <summary>
/// Represents an RGB color specified as a tuple of 3 values - the levels of
/// <b>red</b>, <b>green</b> and <b>blue</b>.
/// </summary>
/// <remarks>
/// Each of the 3 color levels is specified through an integral value ranging
/// from 0 to 255.
/// <para>
/// Since this class inherits an <see cref="AnsiArgumentSequence"/>, it should
/// be used as part of an <see cref="AnsiControlSequence"/> for changing the
/// color of text. An RGB color argument sequence has the following structure:
/// <code>
/// <i>color category</i>;2;<i>red level</i>;<i>green level</i>;
/// <i>blue level</i>
/// </code>
/// The code <c>2</c> specifies that the color is an RGB color. See
/// <see cref="RGBColorTypeCode"/> for further information.
/// </para>
/// </remarks>
public sealed class RGBColor : AnsiArgumentSequence
{
    /// <summary>
	/// Represents the part of the ANSI argument sequence which specifies that the
	/// color is an RGB color.
	/// </summary>
	/// <remarks>
	/// This code comes second in the argument sequence after the color category
	/// code. As an example, the following sequence can be used to set the
	/// background color to green using an RGB color: <c>48;2;0;255;0</c>.
	/// </remarks>
    public const byte RGBColorTypeCode = 2;

    /// <summary>
    /// Represents the color's level of red, which can range from 0 to 255.
    /// </summary>
    public byte Red { get; }
    /// <summary>
    /// Represents the color's level of green, which can range from 0 to 255.
    /// </summary>
    public byte Green { get; }
    /// <summary>
    /// Represents the color's level of blue, which can range from 0 to 255.
    /// </summary>
    public byte Blue { get; }

    /// <summary>
    /// Construct an RGB color.
    /// </summary>
    /// <param name="red">The level of red</param>
    /// <param name="green">The level of green</param>
    /// <param name="blue">The level of blue</param>
    public RGBColor(byte red, byte green, byte blue)
        : base(RGBColorTypeCode, red, green, blue) =>
        (Red, Green, Blue) = (red, green, blue);

    /// <summary>
    /// Allows the deconstruction of an <see cref="RGBColor"/> into a tuple of 3
    /// values.
    /// </summary>
    /// <returns>The levels of red, green and blue</returns>
    public (byte, byte, byte) Deconstruct() => (Red, Green, Blue);

    /// <summary>
    /// Allows explicit conversion of an <see cref="RGBColor"/> to a tuple of 3
    /// values.
    /// </summary>
    /// <param name="color">The levels of red, green and blue</param>
    public static explicit operator (byte, byte, byte)(RGBColor color)
        => (color.Red, color.Green, color.Blue);
}
