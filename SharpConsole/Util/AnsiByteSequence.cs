namespace SharpConsole.Util;

/// <summary>
/// Represents the byte sequence part of an
/// <see href="https://en.wikipedia.org/wiki/ANSI_escape_code">ANSI escape
/// sequence.</see>
/// </summary>
/// <remarks>
/// The byte sequence represents a list of one or more parameter values on which
/// an operation (specified through an <see cref="AnsiCommand"/>) will be
/// performed.
/// <para>
/// An ANSI byte sequence is of no use by itself, and is usually only used to
/// construct an ANSI escape sequence (<see cref="AnsiEscapeSequence"/>).
/// </para>
/// </remarks>
public class AnsiByteSequence
{
    /// <summary>
    /// Used for separating the byte codes of a byte sequence.
    /// </summary>
    public const char ByteCodeSeparator = ';';

    /// <summary>
    /// Contains the individual byte codes of the sequence.
    /// </summary>
    public IReadOnlyCollection<byte> ByteCodes { get; }

    /// <summary>
    /// Construct an ANSI byte sequence.
    /// </summary>
    /// <param name="byteCodes">the byte codes of the sequence</param>
    /// <exception cref="ArgumentNullException">
    ///     if the byte code array is null
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     if the byte code array is empty
    /// </exception>
	public AnsiByteSequence(params byte[] byteCodes)
	{
		ArgumentNullException.ThrowIfNull(nameof(byteCodes));

        if (byteCodes.Length == 0)
        {
            throw new ArgumentException("Parameter cannot be empty.", nameof(byteCodes));
        }

        ByteCodes = byteCodes;
        _byteSequence = new(GenerateByteSequence);
    }

    /// <summary>
    /// Used for caching the string byte sequence so as to avoid multiple calls to
    /// <see cref="GenerateByteSequence"/>.
    /// </summary>
    private readonly Lazy<string> _byteSequence;

    /// <summary>
    /// Convert the <see cref="AnsiByteSequence"/> instance to a string.
    /// </summary>
    /// <remarks>
    /// See <see cref="GenerateByteSequence"/> for further details.
    /// </remarks>
    /// <returns>the byte sequence</returns>
    public override string ToString() => _byteSequence.Value;

    /// <summary>
    /// Converts the byte codes (<see cref="ByteCodes"/>) to a string of bytes
    /// separated by semicolons (';').
    /// </summary>
    /// <remarks>
    /// This function is only used for initializing the cache object
    /// (<see cref="_byteSequence"/>).
    /// </remarks>
    /// <returns>the byte sequence</returns>
    private string GenerateByteSequence() =>
        string.Join(ByteCodeSeparator, ByteCodes);
}
