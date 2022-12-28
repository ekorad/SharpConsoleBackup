namespace SharpConsole.Util;

/// <summary>
/// Represents the argument sequence part of an
/// <see href="https://en.wikipedia.org/wiki/ANSI_escape_code">ANSI control
/// sequence.</see>
/// </summary>
/// <remarks>
/// The argument sequence is a list of one or more byte values on which an
/// operation (specified through <see cref="AnsiCommand"/>) will be performed.
/// The individual bytes are separated by semicolons ('<c>;</c>').
/// <para>
/// The following is an example of an argument sequence which can be used to set
/// the foreground color of the text to green: <c>38;2;0;255;0</c>.
/// </para>
/// <para>
/// An ANSI argument sequence has no purpose by itself, and is mostly used to
/// build an ANSI control sequence (<see cref="AnsiControlSequence"/>).
/// </para>
/// </remarks>
public class AnsiArgumentSequence
{
    /// <summary>
    /// Used for separating the individual arguments of a sequence.
    /// </summary>
    public const char ArgumentSeparator = ';';

    /// <summary>
    /// Contains the individual argument values of the sequence.
    /// </summary>
    public IReadOnlyCollection<byte> Arguments { get; }

    /// <summary>
    /// Construct an ANSI argument sequence.
    /// </summary>
    /// <param name="arguments">The argument values of the sequence</param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="arguments"/> is null
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Thrown if <paramref name="arguments"/> is empty
    /// </exception>
	public AnsiArgumentSequence(params byte[] arguments)
	{
		ArgumentNullException.ThrowIfNull(nameof(arguments));

        if (arguments.Length == 0)
        {
            throw new ArgumentException("Parameter cannot be empty.", nameof(arguments));
        }

        Arguments = arguments;
        _byteSequence = new(GenerateArgumentSequence);
    }

    /// <summary>
    /// Used for caching the string representation of the instance.
    /// </summary>
    /// <remarks>
    /// See <see cref="GenerateArgumentSequence"/> for further details regarding the
    /// structure of the string.
    /// </remarks>
    private readonly Lazy<string> _byteSequence;

    /// <summary>
    /// Get the string representation of the instance.
    /// </summary>
    /// <remarks>
    /// See <see cref="GenerateArgumentSequence"/> for further details regarding the
    /// structure of the string.
    /// </remarks>
    /// <returns>The string representation of the argument sequence</returns>
    public override string ToString() => _byteSequence.Value;

    /// <summary>
    /// Converts the argument values (<see cref="Arguments"/>) to a string of byte
    /// values separated by semicolons ('<c>;</c>').
    /// </summary>
    /// <remarks>
    /// The following is an example of an argument sequence which can be used to set
    /// the foreground color of the text to green: <c>38;2;0;255;0</c>.
    /// <para>
    /// This function is only used for initializing the string representation cache
    /// object (<see cref="_byteSequence"/>).
    /// </para>
    /// </remarks>
    /// <returns>The string representation of the argument sequence</returns>
    private string GenerateArgumentSequence() =>
        string.Join(ArgumentSeparator, Arguments);
}
