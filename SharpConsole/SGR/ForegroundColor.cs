﻿using SharpConsole.Util;

namespace SharpConsole.SGR;

/// <summary>
/// Represents the foreground color of text.
/// </summary>
/// <remarks>
/// Since this class inherits an <see cref="AnsiArgumentSequence"/>, it should
/// be used as part of an <see cref="AnsiControlSequence"/> for changing the
/// foreground color of text.
/// <para>
/// The actual type of the color (indexed or RGB) is specified through
/// <typeparamref name="TColor"/>.
/// </para>
/// </remarks>
/// <typeparam name="TColor">The color type</typeparam>
public sealed class ForegroundColor<TColor> : TextColor<TColor>
    where TColor : AnsiArgumentSequence
{
    /// <summary>
    /// Construct a foreground text color object.
    /// </summary>
    /// <param name="color">The foreground color</param>
    public ForegroundColor(TColor color)
        : base(SGRColorCategory.ForegroundColor, color)
    {
    }
}
