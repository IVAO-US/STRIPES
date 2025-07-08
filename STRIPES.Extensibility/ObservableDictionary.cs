using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace STRIPES.Extensibility;

public sealed class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : notnull
{
	public event EventHandler<ObservableDictionaryModifiedEventArgs<TKey, TValue>>? Modified;
	private readonly Dictionary<TKey, TValue> _backing;

	public ObservableDictionary(Dictionary<TKey, TValue> backing) => _backing = backing;
	public ObservableDictionary() : this([]) { }

	public void Add(TKey key, TValue value) => Add(new(key, value));

	public bool ContainsKey(TKey key) => _backing.ContainsKey(key);

	public bool Remove(TKey key) =>
		_backing.Remove(key, out var value) && Update(
			ObservableDictionaryModifiedEventArgs<TKey, TValue>.Modification.Removed,
			new(key, value),
			() => true
		);

#pragma warning disable CS8762 // Parameter must have a non-null value when exiting in some condition.
	public bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue? value) =>
		_backing.TryGetValue(key, out value);
#pragma warning restore

	public TValue this[TKey key]
	{
		get => _backing[key];
		set => Update(
			ObservableDictionaryModifiedEventArgs<TKey, TValue>.Modification.Modified,
			new(key, value),
			() => _backing[key] = value
		);
	}

	public ICollection<TKey> Keys => _backing.Keys;

	public ICollection<TValue> Values => _backing.Values;

	public void Add(KeyValuePair<TKey, TValue> item) => Update(
		ObservableDictionaryModifiedEventArgs<TKey, TValue>.Modification.Added,
		item,
		() => _backing.Add(item.Key, item.Value)
	);

	public void Clear() => Update(
		ObservableDictionaryModifiedEventArgs<TKey, TValue>.Modification.Cleared,
		null,
		_backing.Clear
	);

	public bool Contains(KeyValuePair<TKey, TValue> item) => _backing.Contains(item);

	public void CopyTo(KeyValuePair<TKey, TValue>[] collection, int startIdx)
	{
		if (collection.Length < _backing.Count)
			throw new ArgumentOutOfRangeException(nameof(collection), "The provided collection isn't big enough to receive the data.");

		else if (collection.Length - startIdx < _backing.Count)
			throw new ArgumentOutOfRangeException(nameof(startIdx), "The provided start index doesn't leave enough room for the data.");

		foreach (var kvp in _backing)
			collection[startIdx++] = kvp;
	}

	public bool Remove(KeyValuePair<TKey, TValue> item) =>
		TryGetValue(item.Key, out var checkVal) && checkVal.Equals(item.Value) && Remove(item.Key);

	public int Count => _backing.Count;

	public bool IsReadOnly => false;

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => ((IEnumerable<KeyValuePair<TKey, TValue>>)_backing).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_backing).GetEnumerator();

	private void Update(ObservableDictionaryModifiedEventArgs<TKey, TValue>.Modification type, KeyValuePair<TKey, TValue>? entry, Action update)
	{
		update();
		Modified?.Invoke(this, new(type, entry));
	}

	private T Update<T>(ObservableDictionaryModifiedEventArgs<TKey, TValue>.Modification type, KeyValuePair<TKey, TValue>? entry, Func<T> update)
	{
		T result = update();
		Modified?.Invoke(this, new(type, entry));
		return result;
	}
}

public sealed record ObservableDictionaryModifiedEventArgs<TKey, TValue>(
	ObservableDictionaryModifiedEventArgs<TKey, TValue>.Modification Action,
	KeyValuePair<TKey, TValue>? Entry
) where TKey : notnull
{
	public enum Modification
	{
		Added,
		Modified,
		Removed,
		Cleared
	}
}
