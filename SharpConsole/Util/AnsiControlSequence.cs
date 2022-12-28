namespace SharpConsole.Util;

/// <summary>
/// Represents an <see href="https://en.wikipedia.org/wiki/ANSI_escape_code">
/// ANSI control sequence</see> which can be used for performing various
/// console-related operations.
/// </summary>
/// <remarks>
/// An ANSI control sequence is used to control various aspects of the console
/// such as cursor position, color, font styling, and others.
/// <para>
/// The structure of an ANSI control sequence is as follows:
/// <list type="bullet">
///     <item>
///         the control sequence introducer (<see cref="ControlSequenceIntroducer"/>),
///     </item>
///     <item>
///         a sequence of argument values separated by semicolons ('<c>;</c>'),
///     </item>
///     <item>
///         an ANSI command code which specifies what operation will be performed on the
///         aforementioned arguments.
///     </item>
/// </list>
/// </para>
/// </remarks>
public class AnsiControlSequence
{
    /// <summary>
    /// Represents the first part of any ANSI control sequence.
    /// </summary>
    public const string ControlSequenceIntroducer = "\x1b[";

    /// <summary>
    /// Specifies what operation should be performed on the arguments
    /// (<see cref="ArgumentSequences"/>).
    /// </summary>
    public AnsiCommand Command { get; }

    /// <summary>
    /// Stores all of the sequence's argument values.
    /// </summary>
    public IReadOnlyCollection<AnsiArgumentSequence> ArgumentSequences { get; }

    /// <summary>
    /// Used for caching the string representation of the instance.
    /// </summary>
    /// <remarks>
    /// See <see cref="GenerateControlSequence"/> for further details regarding the
    /// structure of the string.
    /// </remarks>
    private readonly Lazy<string> _controlSequence;

    /// <summary>
    /// Construct a complete (usable) ANSI control sequence.
    /// </summary>
    /// <remarks>
    /// The <paramref name="arguments"/> parameter is used to populate a single
    /// <see cref="AnsiArgumentSequence"/> associated to this instance.
    /// </remarks>
    /// <param name="command">The operation that will be performed</param>
    /// <param name="arguments">The argument values</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if <paramref name="command"/> is not a valid enum value
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="arguments"/> is null
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Thrown if <paramref name="arguments"/> is empty
    /// </exception>
    public AnsiControlSequence(AnsiCommand command, params byte[] arguments)
        : this(command, new AnsiArgumentSequence(arguments))
    {
    }

    /// <summary>
    /// Construct a complete (usable) ANSI control sequence.
    /// </summary>
    /// <remarks>
    /// This constructor allows the association of multiple
    /// <see cref="AnsiArgumentSequence"/> to this instance through the
    /// <paramref name="argumentSequences"/> parameter.
    /// </remarks>
    /// <param name="command">The operation that will be performed</param>
    /// <param name="argumentSequences">The argument value sequences</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if <paramref name="command"/> is not a valid enum value
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="argumentSequences"/> is null
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Thrown if <paramref name="argumentSequences"/> is empty
    /// </exception>
    public AnsiControlSequence(AnsiCommand command,
        params AnsiArgumentSequence[] argumentSequences)
    {
        ArgumentNullException.ThrowIfNull(nameof(argumentSequences));

        if (argumentSequences.Length == 0)
        {
            throw new ArgumentException("Parameter cannot be empty.", nameof(argumentSequences));
        }

        if (!Enum.IsDefined(typeof(AnsiCommand), command))
        {
            throw new ArgumentOutOfRangeException(nameof(command));
        }

        ArgumentSequences = argumentSequences;
        Command = command;
        _controlSequence = new(GenerateControlSequence);
    }

    /// <summary>
    /// Get the string representation of the instance.
    /// </summary>
    /// <remarks>
    /// See <see cref="GenerateControlSequence"/> for further details regarding the
    /// structure of the string.
    /// </remarks>
    /// <returns>The string representation of the control sequence</returns>
    public override string ToString() => _controlSequence.Value;

    /// <summary>
    /// Allows the direct conversion of an <see cref="AnsiControlSequence"/> to a
    /// string.
    /// </summary>
    /// <param name="sequence">The sequence to be converted</param>
    public static implicit operator string(AnsiControlSequence sequence) =>
        sequence._controlSequence.Value;

    /// <summary>
    /// Appends the <see cref="ControlSequenceIntroducer"/> and the associated ANSI
    /// command to the joined argument values sequence
    /// (<see cref="GetJoinedArgumentValues"/>).
    /// </summary>
    /// <returns>The string representation of the control sequence</returns>
    private string GenerateControlSequence() =>
        ControlSequenceIntroducer + GetJoinedArgumentValues() + (char)Command;

    /// <summary>
    /// Join all argument values from all associated argument sequences by
    /// semicolons ('<c>;</c>').
    /// </summary>
    /// <returns>All argument values joined by '<c>;</c>'</returns>
    private string GetJoinedArgumentValues() =>
        string.Join(AnsiArgumentSequence.ArgumentSeparator,
            (from sequence in ArgumentSequences select sequence.ToString()));
}
