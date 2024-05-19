using System.Collections.Generic;

namespace LibraryManagement
{
    public class CustomList<T>
    {
        private List<Node<T>> items = new List<Node<T>>();

        public void Add(T item)
        {
            Node<T> node = new Node<T>(item);
            items.Add(node);
        }

        public bool Remove(T item)
        {
            var node = items.Find(n => n.Data.Equals(item));
            if (node != null)
            {
                items.Remove(node);
                return true;
            }
            return false;
        }

        public T Find(Predicate<T> match)
        {
            var node = items.Find(n => match(n.Data));
            return node != null ? node.Data : default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in items)
            {
                yield return node.Data;
            }
        }
    }

    public class Node<T>
    {
        public T Data;
        public Node(T data) { Data = data; }
    }
}
