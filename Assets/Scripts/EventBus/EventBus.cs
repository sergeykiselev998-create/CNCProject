using System;
using System.Collections.Generic;
using CNC.Interfaces.Events;

public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<HandlerEntry>> _handlers = new();
        
        public IDisposable Subscribe<T>(Action<T> handler, int priority = 0) where T : IEventMessage
        {
            var type = typeof(T);

            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<HandlerEntry>();

            var entry = new HandlerEntry 
            { 
                Priority = priority, 
                Delegate = handler 
            };

            _handlers[type].Add(entry);
            
            // Сортируем по приоритету (меньшее число = выше приоритет)
            _handlers[type].Sort((a, b) => a.Priority.CompareTo(b.Priority));

            // Возвращаем наш кастомный токен отписки
            return new SubscriptionToken(() => UnsubscribeInternal(type, entry));
        }

        /// <summary>
        /// Публикация события.
        /// </summary>
        public void Publish<T>(T evt) where T : IEventMessage
        {
            var type = typeof(T);

            if (_handlers.TryGetValue(type, out var list))
            {
                // Снимок списка для безопасности при изменении коллекции во время вызова
                var snapshot = new List<HandlerEntry>(list); 

                foreach (var entry in snapshot)
                {
                    if (entry.Delegate is Action<T> action)
                    {
                        action.Invoke(evt);
                    }
                }
            }
        }

        private void UnsubscribeInternal(Type type, HandlerEntry entry)
        {
            if (_handlers.TryGetValue(type, out var list))
            {
                list.Remove(entry);
            }
        }

        private class HandlerEntry
        {
            public int Priority { get; set; }
            public Delegate Delegate { get; set; }
        }
    }