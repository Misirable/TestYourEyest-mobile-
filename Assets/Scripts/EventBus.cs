using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public static class EventBus
{
	private class CallbackGroupNoArgs
	{
		private List<Action> _callbacks = new List<Action>();

		public void Add(Action callback)
		{
			_callbacks.Add(callback);
		}

		public void Remove(Action callback)
		{
			_callbacks.Remove(callback);
		}

		public bool Contains(Action callback)
		{
			return _callbacks.Contains(callback);
		}

		public IEnumerable<Action> GetCallbacks()
		{
			return _callbacks;
		}
	}

	private class CallbackGroup<TArg>
	{
		private List<Action<TArg>> _callbacks = new List<Action<TArg>>();

		public void Add(Action<TArg> callback)
		{
			_callbacks.Add(callback);
		}

		public void Remove(Action<TArg> callback)
		{
			_callbacks.Remove(callback);
		}

		public bool Contains(Action<TArg> callback)
		{
			return _callbacks.Contains(callback);
		}

		public IEnumerable<Action<TArg>> GetCallbacks()
		{
			return _callbacks;
		}
	}

	private static Dictionary<string, object> _eventsToCallbacks =
		new Dictionary<string, object>();

	public static void Subscribe(string eventName, Action callback)
	{
		CallbackGroupNoArgs callbacks;

		if (!_eventsToCallbacks.ContainsKey(eventName))
			_eventsToCallbacks.Add(eventName, callbacks = new CallbackGroupNoArgs());

		callbacks = _eventsToCallbacks[eventName] as CallbackGroupNoArgs;
		if (callback == null)
			throw new ArgumentException(
				$"Event {eventName} is needed to subscribe funcs with generic arguments " +
				$"{string.Join(", ", callback.GetType().GetGenericArguments().Select(t => t.Name))}");

		callbacks.Add(callback);
	}

	public static void Subscribe<TArg>(string eventName, Action<TArg> callback)
	{
		CallbackGroup<TArg> callbacks;

		if (!_eventsToCallbacks.ContainsKey(eventName))
			_eventsToCallbacks.Add(eventName, callbacks = new CallbackGroup<TArg>());

		callbacks = _eventsToCallbacks[eventName] as CallbackGroup<TArg>;
		if (callback == null)
			throw new ArgumentException(
				$"Event {eventName} is needed to subscribe funcs with generic arguments " +
				$"{string.Join(", ", _eventsToCallbacks[eventName].GetType().GetGenericArguments().Select(t => t.Name))}");

		callbacks.Add(callback);
	}

	public static void Unsubscribe(string eventName, Action callback)
	{
		if (!_eventsToCallbacks.ContainsKey(eventName))
			return;

		CallbackGroupNoArgs group = _eventsToCallbacks[eventName] as CallbackGroupNoArgs;
		if (group == null)
			throw new ArgumentException(
				$"Callback does not correspond required generic types: " +
				$"{string.Join(", ", _eventsToCallbacks[eventName].GetType().GetGenericArguments().Select(t => t.Name))}");

		if (!group.Contains(callback))
			throw new InvalidOperationException($"Event \"{eventName}\" is not previously registered");

		group.Remove(callback);

		if (group.GetCallbacks().Count() != 0)
			return;

		_eventsToCallbacks.Remove(eventName);
	}

	public static void Unsubscribe<TArg>(string eventName, Action<TArg> callback)
	{
		if (!_eventsToCallbacks.ContainsKey(eventName))
			throw new ArgumentException("This callback was not previously subscribed this event");

		CallbackGroup<TArg> group = _eventsToCallbacks[eventName] as CallbackGroup<TArg>;

		if (group == null)
		{
			string sxcMessage = _eventsToCallbacks[eventName] is CallbackGroupNoArgs ?
				"Callbacks of this event do not accept any arguments" :
				$"Argument does not correspond required type: " +
				$"{_eventsToCallbacks[eventName].GetType().GetGenericArguments()[0].Name}";

			throw new ArgumentException(sxcMessage);
		}

		if (!group.Contains(callback))
			throw new InvalidOperationException($"Event \"{eventName}\" is not previously registered");

		group.Remove(callback);

		if (group.GetCallbacks().Count() != 0)
			return;

		_eventsToCallbacks.Remove(eventName);
	}

	public static void Unsubscribe(string eventName)
	{
		_eventsToCallbacks.Remove(eventName);
	}

	public static void Broadcast(string eventName)
	{
		if (!_eventsToCallbacks.ContainsKey(eventName))
			return;

		CallbackGroupNoArgs group = _eventsToCallbacks[eventName] as CallbackGroupNoArgs;

		if (group == null)
			throw new ArgumentException(
				$"Argument does not correspond required type: " +
				$"{_eventsToCallbacks[eventName].GetType().GetGenericArguments()[0].Name}");

		foreach (Action callback in group.GetCallbacks())
			callback.Invoke();
	}

	public static void Broadcast<TArg>(string eventName, TArg arg)
	{
		if (!_eventsToCallbacks.ContainsKey(eventName))
			return;

		CallbackGroup<TArg> group = _eventsToCallbacks[eventName] as CallbackGroup<TArg>;

		if (group != null)
		{
			foreach (Action<TArg> callback in group.GetCallbacks())
				callback.Invoke(arg);

			return;
		}

		string sxcMessage = _eventsToCallbacks[eventName] is CallbackGroupNoArgs ?
			"Callbacks of this event do not accept any arguments" :
			$"Argument does not correspond required type: " +
			$"{_eventsToCallbacks[eventName].GetType().GetGenericArguments()[0].Name}";

		throw new ArgumentException(sxcMessage);
	}
}
