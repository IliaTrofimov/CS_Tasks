using System.Collections;

namespace task_4
{
    public class SplayTreeEnumerator<T> : IEnumerator<T> where T: IComparable<T>
    {
        public SplayTreeNode<T> CurrentNode { get; set; }
        private Stack<SplayTreeNode<T>> stack;
        private SplayTreeNode<T> ouput;


        public SplayTreeEnumerator(SplayTreeNode<T> root)
        {
            stack = new Stack<SplayTreeNode<T>>();
            CurrentNode = root;
        }

        public void Dispose()
        {
            stack = null;
        }

        public bool MoveNext()
        {
            while (stack.Count > 0 || CurrentNode != null)
            {
                if (CurrentNode != null)
                {
                    stack.Push(CurrentNode);
                    CurrentNode = CurrentNode.Left;
                }
                else
                {
                    CurrentNode = stack.Pop();
                    ouput = CurrentNode;
                    CurrentNode = CurrentNode.Right;
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            stack = new Stack<SplayTreeNode<T>>();
        }

        public T Current => ouput.Data;

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}