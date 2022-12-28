namespace SharpConsole.Util;

/// <summary>
/// Represents a generic ANSI sequence which can be outputted to the console for
/// controlling cursor location, color, font styling, and other options.
/// </summary>
/// <remarks>
/// An ANSI sequence is composed of the following:
/// <list type="bullet">
///     <item>
///         <term>The sequence prefix ("\x1b[")</term>
///         <description>a standard escape sequence</description>
///     </item>
///     <item>
///         <term>A sequence of bytes separated by semicolons (';')</term>
///         <description>specifies the values of the parameters</description>
///     </item>
///     <item>
///         <term>A command code</term>
///         <description>
///             specifies what operation will be performed on the aforementioned parameter
///             values
///         </description>
///     </item>
/// </list>
/// See <seealso href="https://en.wikipedia.org/wiki/ANSI_escape_code">
/// ANSI Escape Code</seealso> for further details.
/// </remarks>
internal class AnsiEscapeSequence
{
    /// <summary>
    /// Represents the beginning of any ANSI sequence.
    /// </summary>
    public const string SequencePrefix = "\x1b[";

    /// <summary>
    /// Specifies what operation should be performed on the byte sequence
    /// (<see cref="ByteSequence"/>).
    /// </summary>
    public AnsiCommand Command { get; }

    /// <summary>
    /// Represents the byte sequence part.
    /// </summary>
    public AnsiByteSequence ByteSequence { get; }

    /// <summary>
    /// Used for caching the value of the string sequence so as to avoid multiple
    /// calls to <see cref="GenerateEscapeSequence"/>.
    /// </summary>
    private readonly Lazy<string> _escapeSequence;

    /// <summary>
    /// Construct a complete, usable, ANSI sequence.
    /// </summary>
    /// <param name="command">the operation that will be performed</param>
    /// <param name="byteCodes">the parameter values of the sequence</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     if the supplied ANSI command is not a valid enum value
    /// </exception>
    public AnsiEscapeSequence(AnsiCommand command, params byte[] byteCodes)
        : this(command, new AnsiByteSequence(byteCodes))
    {
    }

    /// <summary>
    /// Construct a complete, usable, ANSI sequence.
    /// </summary>
    /// <param name="command">the operation that will be performed</param>
    /// <param name="byteSequence">the byte sequence</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     if the supplied ANSI command is not a valid enum value
    /// </exception>
    public AnsiEscapeSequence(AnsiCommand command, AnsiByteSequence byteSequence)
    {
        if (!Enum.IsDefined(typeof(AnsiCommand), command))
        {
            throw new ArgumentOutOfRangeException(nameof(command));
        }

        ByteSequence = byteSequence;
        Command = command;
        _escapeSequence = new(GenerateEscapeSequence);
    }

    /// <summary>
    /// Convert the <see cref="AnsiEscapeSequence"/> instance to a string.
    /// </summary>
    /// <remarks>
    /// See <see cref="GenerateEscapeSequence"/> for further details.
    /// </remarks>
    /// <returns>the complete ANSI escape sequence</returns>
    public override string ToString() => _escapeSequence.Value;

    /// <summary>
    /// Allows the direct conversion of an <see cref="AnsiEscapeSequence"/> to a
    /// string.
    /// </summary>
    /// <param name="sequence">the sequence to be converted</param>
    public static implicit operator string(AnsiEscapeSequence sequence) =>
        sequence._escapeSequence.Value;

    /// <summary>
    /// Appends the "\x1b[" escape sequence and the ANSI command to the byte
    /// sequence (<see cref="AnsiByteSequence"/>).
    /// </summary>
    /// <returns>the complete ANSI escape sequence</returns>
    private string GenerateEscapeSequence() =>
        SequencePrefix + ByteSequence.ToString() + (char)Command;
}
