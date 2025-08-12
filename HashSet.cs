class MyHashSet
{
    private SingleLinkedList<int>[] buckets;
    private int cap;
    private int count;
    private float loadFactor;

    public int Capacity => cap;

    public MyHashSet(float loadFactor = 0.75f)
    {
        cap = 5;
        this.loadFactor = loadFactor;
        count = 0;
        buckets = new SingleLinkedList<int>[cap];

        for (int i = 0; i < cap; i++)
        {
            buckets[i] = new SingleLinkedList<int>();
        }
    }


    private int HashCode(int key, int mod)
    {
        return (key & 0x7FFFFFFF) % mod;
    }

    private int GetBucketIndex(int key)
    {
        return HashCode(key, cap);
    }

    public void Add(int key)
    {
        if (Contains(key)) return;

        if ((float)(count + 1) / cap > loadFactor)
        {
            Resize();
        }

        buckets[GetBucketIndex(key)].Push_Front(key);
        count++;
    }

    public void Remove(int key)
    {
        if (Contains(key))
        {
            buckets[GetBucketIndex(key)].DeleteByValue(key);
            count--;
        }
    }

    public bool Contains(int key)
    {
        Node<int> current = buckets[GetBucketIndex(key)].Head;

        while (current != null)
        {
            if (current.Value == key)
                return true;
            current = current.Next;
        }
        return false;
    }

    public int Count()
    {
        return count;
    }

    private void Resize()
    {
        int newCapacity = cap * 2;
        SingleLinkedList<int>[] newBuckets = new SingleLinkedList<int>[newCapacity];

        for (int i = 0; i < newCapacity; i++)
        {
            newBuckets[i] = new SingleLinkedList<int>();
        }

        for (int i = 0; i < cap; i++)
        {
            Node<int> current = buckets[i].Head;

            while (current != null)
            {
                int newIndex = HashCode(current.Value, newCapacity);
                newBuckets[newIndex].Push_Front(current.Value);

                current = current.Next;
            }
        }

        buckets = newBuckets;


        cap = newCapacity;
    }


    public void Print()
    {
        for (int i = 0; i < cap; i++)
        {
            Console.Write($"Bucket {i}: ");
            buckets[i].Print();
        }
    }
}
