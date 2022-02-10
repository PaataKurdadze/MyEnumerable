using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Test_Enumerable
{
    public static class MyEnumerable
    {
        public static T MyAggregate<T>(this IEnumerable<T> source, Func<T, T, T> func)
        {
            if (source == null || !source.Any() || func == null) throw new ArgumentNullException();

            var e = source.GetEnumerator();
            var result = e.Current;

            while (e.MoveNext())
                result = func(result, e.Current);

            return result;
        }


        public static IEnumerable<TResult> MySelectMany<T, TResult>(this IEnumerable<T> source,
            Func<T, IEnumerable<TResult>> selector)
        {
            if (source == null || selector == null) throw new ArgumentNullException();

            foreach (var item in source)
                foreach (var result in selector(item))
                    yield return result;
        }


        public static Dictionary<TKey, TSource> MyToDictionary<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (source == null || keySelector == null) throw new ArgumentNullException();

            var dictionary = new Dictionary<TKey, TSource>();
            foreach (var element in source)
                dictionary.Add(keySelector(element), element);
            return dictionary;
        }


        public static Dictionary<TKey, TElement> MyToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source == null || keySelector == null || elementSelector == null) throw new ArgumentNullException();

            var dictionary = new Dictionary<TKey, TElement>();
            foreach (var element in source)
                dictionary.Add(keySelector(element), elementSelector(element));
            return dictionary;
        }


        public static IEnumerable<(TFirst First, TSecond Second)> MyZip<TFirst, TSecond>(this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            var eFirst = first.GetEnumerator();
            var eSecond = second.GetEnumerator();

            while (eFirst.MoveNext() && eSecond.MoveNext())
                yield return (eFirst.Current, eSecond.Current);
        }

        public static IEnumerable<TResult> MyZip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null || second == null || resultSelector == null) throw new ArgumentNullException();

            var eFirst = first.GetEnumerator();
            var eSecond = second.GetEnumerator();

            while (eFirst.MoveNext() && eSecond.MoveNext())
                yield return resultSelector(eFirst.Current, eSecond.Current);
        }


        public static IEnumerable<IGrouping<TKey, TSource>> MyGroupBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (source == null || keySelector == null) throw new ArgumentNullException();

            foreach (var item in GetGroupedCollection(source, keySelector))
                yield return new Grouping<TKey, TSource>(item.Key, item.Value);
        }


        public static IEnumerable<TResult> MyGroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
        {
            if (source == null || keySelector == null || resultSelector == null) throw new ArgumentNullException();

            foreach (var item in GetGroupedCollection(source, keySelector))
                yield return resultSelector(item.Key, item.Value);
        }

        private static Dictionary<TKey, List<TSource>> GetGroupedCollection<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            Dictionary<TKey, List<TSource>> dictionary = new();

            foreach (var item in source)
            {
                var keyCurrent = keySelector(item);

                if (dictionary.ContainsKey(keyCurrent)) dictionary[keyCurrent].Add(item);
                else dictionary.Add(keyCurrent, new() { item });
            }

            return dictionary;
        }


        public static IEnumerable<TResult> MyOfType<TResult>(this IEnumerable source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            foreach (var item in source)
                if (item is TResult result) yield return result; 
        }
        
    }

    internal class Grouping<TKey, TSource> : IGrouping<TKey, TSource>
    {
        private TKey _key;
        private IEnumerable<TSource> _elements;

        public TKey Key { get; }

        public Grouping(TKey key, IEnumerable<TSource> elements)
        {
            _key = key;
            _elements = elements;
        }

        public IEnumerator<TSource> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}





/*
 *  public static IEnumerable<IGrouping<TKey, TSource>> MyGroupBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            List<Grouping<TKey, TSource>> list = new();

            foreach (var itemS in source)
            {
               
                var keyCurrent = keySelector(itemS);
                if(!list.Any()) list.Add(new Grouping<TKey, TSource>(keyCurrent, itemS as IEnumerable<TSource>));
                foreach (var element in list)
                {
                    if (keyCurrent.Equals(element.Key))
                    {
                        element._elements.ToList().Add(itemS);
                    }
                    else
                    {
                        list.Add(new Grouping<TKey, TSource>(keyCurrent, itemS as IEnumerable<TSource>));
                    }
                }
            }

            return list;
        }

 */