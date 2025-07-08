using STRIPES.Extensibility;

using System.Reflection;

namespace STRIPES.Commands;

internal class CommandContainer
{
	private readonly Dictionary<Type, IOmnibarCommand> _activeCommands = [],
													   _inactiveCommands = [];

	private readonly Dictionary<Type, object> _providedDependencies = [];

	private HashSet<Type> AvailableTypes => [
		.._activeCommands.Keys,
		.._inactiveCommands.Keys,
		.._providedDependencies.Keys
	];

	public CommandContainer()
	{
		// Get all types in the current assembly that match.
		HashSet<Type> typesToInstance = [.. typeof(OmnibarService).Assembly.GetTypes().Where(t =>
			t.IsAssignableTo(typeof(IOmnibarCommand))
		)];

		// Instantiate as many as you can.
		int typeCount = int.MaxValue;
		while (typesToInstance.Count < typeCount && typesToInstance.Count is not 0)
		{
			typeCount = typesToInstance.Count;

			// Find any types that can be instanced as-is.
			var typeDict = typesToInstance.GroupBy(CanInstance).ToDictionary(static g => g.Key ?? -1, static g => g.AsEnumerable());

			if (typeDict.Keys.Count is 1 && typeDict.Keys.Single() is -1)
				throw new Exception($"Unable to instantiate all commands for injection. Failed on: {string.Join(", ", typeDict.Values.Single())}");

			foreach (Type type in typeDict[typeDict.Keys.Where(k => k >= 0).Min()])
			{
				EnableCommand(Instance(type));
				typesToInstance.Remove(type);
			}
		}
	}

	/// <summary>
	/// Gets all active <see cref="IOmnibarCommand"/> instances matching the provided <paramref name="target"/>.
	/// </summary>
	public IEnumerable<IOmnibarCommand> Get(IOmnibarCommand.CommandTarget target) => _activeCommands.Values.Where(ac => ac.Target.HasFlag(target));

	/// <summary>
	/// Enables an <see cref="IOmnibarCommand"/> in the omnibar.
	/// </summary>
	/// <param name="command">The <see cref="IOmnibarCommand"/> to be registered.</param>
	public void EnableCommand(IOmnibarCommand command)
	{
		Type type = command.GetType();
		_inactiveCommands.Remove(type);
		_activeCommands.Add(type, command);
	}

	/// <summary>
	/// Disables an <see cref="IOmnibarCommand"/> in the omnibar.
	/// </summary>
	public void DisableCommand<T>() where T : IOmnibarCommand
	{
		if (_activeCommands.Remove(typeof(T), out var command))
			_inactiveCommands.Add(typeof(T), command);
	}

	/// <summary>
	/// Determines if a provided type can be instantiated with the currently available plugins.
	/// </summary>
	/// <returns>The number of parameters in the largest constructor signature minus the number of parameters in the largest constructable signature.</returns>
	int? CanInstance(Type type)
	{
		// Get the constructors sorted by signature length.
		ConstructorInfo[] ctors = [.. type.GetConstructors().OrderByDescending(static ctor => ctor.GetParameters().Length)];
		if (ctors.Length is 0)
			return null;

		int maxLen = ctors[0].GetParameters().Length;

		// Find the longest constructor we can satisfy.
		if (ctors.FirstOrDefault(ctor => ctor.GetParameters().All(param => AvailableTypes.Contains(param.ParameterType))) is not ConstructorInfo ctor)
			return null;

		return maxLen - ctor.GetParameters().Length;
	}

	IOmnibarCommand Instance(Type type)
	{
		ConstructorInfo ctor = type.GetConstructors()
			.OrderByDescending(static ctor => ctor.GetParameters().Length)
			.First(ctor => ctor.GetParameters().All(param => AvailableTypes.Contains(param.ParameterType)));

		object[] args = [..ctor.GetParameters().Select(param =>
			_activeCommands.TryGetValue(param.ParameterType, out var ac) ? ac
			: _inactiveCommands.TryGetValue(param.ParameterType, out var ic) ? ic
			: _providedDependencies[param.ParameterType]
		)];

		return (IOmnibarCommand)Activator.CreateInstance(type, args)!;
	}
}
