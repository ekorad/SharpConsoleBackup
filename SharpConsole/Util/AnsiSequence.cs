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
internal class AnsiSequence
{
    /// <summary>
    /// Represents the beginning of any ANSI sequence.
    /// </summary>
    public const string SequencePrefix = "\x1b[";
    /// <summary>
    /// Used for separating the byte codes of the byte sequence.
    /// </summary>
    public const char SequenceParameterSeparator = ';';

    /// <summary>
    /// Specifies what operation should be performed.
    /// </summary>
    public AnsiCommand Command { get; }
    /// <summary>
    /// Contains the byte codes of the byte sequence.
    /// </summary>
    public IReadOnlyCollection<byte> ByteCodes { get; }

    /// <summary>
    /// Used for caching the value of <see cref="ByteSequence"/> so as to avoid
    /// multiple calls to <see cref="GenerateByteSequence"/>.
    /// </summary>
    private readonly Lazy<string> _byteSequence;
    /// <summary>
    /// Represents the byte sequence - a string of byte codes separated by
    /// semicolons (';').
    /// </summary>
    public string ByteSequence => _byteSequence.Value;

    /// <summary>
    /// Used for caching the value of <see cref="EscapeSequence"/> so as to avoid
    /// multiple calls to <see cref="GenerateEscapeSequence"/>.
    /// </summary>
    private readonly Lazy<string> _escapeSequence;
    /// <summary>
    /// Represents the complete, usable ANSI sequence.
    /// </summary>
    public string EscapeSequence => _escapeSequence.Value;

    /// <summary>
    /// Constructs a complete ANSI sequence. The resulting sequence can be used
    /// directly as is.
    /// </summary>
    /// <param name="command">the operation that will be performed</param>
    /// <param name="byteCodes">the parameter values of the sequence</param>
    /// <exception cref="ArgumentNullException">
    ///     if the supplied byte code array is null
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     if the supplied byte code array is empty
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     if the supplied ANSI command is not a valid enum value
    /// </exception>
    public AnsiSequence(AnsiCommand command, params byte[] byteCodes)
    {
        ArgumentNullException.ThrowIfNull(byteCodes, nameof(byteCodes));

        if (byteCodes.Length == 0)
        {
            throw new ArgumentException("Parameter cannot be empty.", nameof(byteCodes));
        }

        if (!Enum.IsDefined(typeof(AnsiCommand), command))
        {
            throw new ArgumentOutOfRangeException(nameof(command));
        }

        Command = command;
        ByteCodes = byteCodes;
        _byteSequence = new(GenerateByteSequence);
        _escapeSequence = new(GenerateEscapeSequence);
    }

    /// <summary>
    /// Is the same as <see cref="EscapeSequence"/>.
    /// </summary>
    /// <returns><see cref="EscapeSequence"/></returns>
    public override string ToString() => EscapeSequence;

    /// <summary>
    /// Allows the <see cref="AnsiSequence"/> to be converted to a string.
    /// </summary>
    /// <param name="sequence">the sequence to be converted</param>
    public static implicit operator string(AnsiSequence sequence) =>
        sequence.EscapeSequence;

    /// <summary>
    /// Converts the byte sequence (<see cref="ByteCodes"/>) to a string of bytes
    /// separated by semicolons (';').
    /// </summary>
    /// <returns>the byte sequence</returns>
    private string GenerateByteSequence() =>
        string.Join(SequenceParameterSeparator, ByteCodes);

    /// <summary>
    /// Appends the "\x1b[" escape sequence and the ANSI command to the byte
    /// sequence (<see cref="ByteSequence"/>).
    /// </summary>
    /// <returns>the complete ANSI sequence</returns>
    private string GenerateEscapeSequence() =>
        SequencePrefix + ByteSequence + (char)Command;
}
