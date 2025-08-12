using System;
using System.Collections.Generic;

class ComparableKeyValuePair<T, V> : IComparable<ComparableKeyValuePair<T, V>>
    where T : IComparable<T>
{
    public T Key   { get; }
    public V Value { get; }

    public ComparableKeyValuePair(T key, V value)
    {
        Key = key;
        Value = value;
    }

    public int CompareTo(ComparableKeyValuePair<T, V> a)
    {
        return Key.CompareTo(a.Key);
    }
    public override string ToString() => ($"{Key} -> {Value}");
}

class Dictionary<T, V> where T : IComparable<T>
{
    private SingleLinkedList<ComparableKeyValuePair<T, V>>[] buckets;
    private int count;
    private float LoadFactor = 0.72f;

    public int Count => count;
    public int Capacity => buckets.Length;

    public Dictionary(int capacity = 7)
    {
        buckets = new SingleLinkedList<ComparableKeyValuePair<T, V>>[capacity];
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new SingleLinkedList<ComparableKeyValuePair<T, V>>();
        }
    }


    private int GetBucketIndex(T key)
    {
        int hash = key.GetHashCode();

        
        return (hash & 0x7FFFFFFF) % buckets.Length;
    }

    private void Resize()
    {
        int newCap = buckets.Length * 2;


        var newBuckets  = new SingleLinkedList<ComparableKeyValuePair<T, V>>[newCap];
        for (int i = 0; i < newCap; i++)
        {
            newBuckets[i] = new SingleLinkedList<ComparableKeyValuePair<T, V>>();
        }
            
        foreach (var bucket in buckets)
        {
            var node = bucket.Head;


            while (node != null)
            {
                int idx = (node.Value!.Key.GetHashCode() & 0x7FFFFFFF) % newCap;
                newBuckets[idx].Push_Front(node.Value);
                node = node.Next;
            }
        }


        buckets = newBuckets;
    }


    public void Add(T key, V value)
    {
        if ((count + 1) / buckets.Length > LoadFactor)
        {
            Resize();
        }

        int index = GetBucketIndex(key);
        var bucket = buckets[index];

        for (var node = bucket.Head; node != null; node = node.Next)
            if (node.Value!.Key.CompareTo(key) == 0)
                throw new ArgumentException("Key already is in dictionary.");

        bucket.Push_Front(new ComparableKeyValuePair<T, V>(key, value));


        count++;
    }

    public bool Remove(T key)
    {
        int index = GetBucketIndex(key);

        var bucket = buckets[index];
        var node = bucket.Head;


        Node<ComparableKeyValuePair<T, V>>? prev = null;

        while (node != null)
        {
            if (node.Value!.Key.CompareTo(key) == 0)
            {
                if (prev == null) bucket.Pop_Front();
                else prev.Next = node.Next;
                count--;
                return true;
            }
            prev = node;
            node = node.Next;
        }

        return false;
    }

    public V Get(T key)
    {
        int index = GetBucketIndex(key);
        
        for (var node = buckets[index].Head; node != null; node = node.Next)
        {
            if (node.Value!.Key.CompareTo(key) == 0)
                return node.Value.Value;
        }

        throw new ArgumentException($"There is no key like {key}");
    }

    public bool ContainsKey(T key)
    {
        int index = GetBucketIndex(key);
        for (var node = buckets[index].Head; node != null; node = node.Next)
        {
            if (node.Value!.Key.CompareTo(key) == 0)
            {
                return true;
            }
        }


        return false;
    }

    public void Print()
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            Console.Write($"Bucket {i} -> ");
            buckets[i].Print();
        }
    }
}
