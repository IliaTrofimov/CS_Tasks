using System.Collections;


namespace task_4
{
    internal enum RotationType
    {
        Right,
        Left
    }

    public class SplayTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private SplayTreeNode<T>? root;


        public void Add(T data)
        {
            var newNode = new SplayTreeNode<T>(data);
            if (Add(newNode) == null)
                return;
            Splay(newNode);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SplayTreeEnumerator<T>(root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



        private SplayTreeNode<T> Add(SplayTreeNode<T> node)
        {
            var newNode = node;
            var actualNode = root;

            if (root == null)
            {
                root = newNode;
                root.Parent = null;
                return newNode;
            }

            while (true)
            {
                if (actualNode.Data.CompareTo(newNode.Data) < 0)
                {
                    if (actualNode.Right == null)
                    {
                        newNode.Parent = actualNode;
                        actualNode.Right = newNode;
                        return newNode;
                    }
                    actualNode = actualNode.Right;
                }
                else
                {
                    if (actualNode.Left == null)
                    {
                        newNode.Parent = actualNode;
                        actualNode.Left = newNode;
                        return newNode;
                    }
                    actualNode = actualNode.Left;
                }
            }
        }

        private void Splay(SplayTreeNode<T> node)
        {
            while (true)
            {
                if (node?.Parent == null) return;
                Rotation(node, LeftPosition(node) ? RotationType.Right : RotationType.Left);
            }
        }

        private bool LeftPosition(SplayTreeNode<T> node)
        {
            return node.Parent.Left == node;
        }

        private void Rotation(SplayTreeNode<T> node, RotationType rotationType)
        {
            if (node.Parent == null) return;

            var rotated = node;
            var parent = node.Parent;

            if (parent.Parent != null)
            {
                if (LeftPosition(parent))
                    parent.Parent.Left = rotated;
                else
                    parent.Parent.Right = rotated;

                rotated.Parent = parent.Parent;
            }
            else
            {
                rotated.Parent = null;
                root = rotated;
            }

            parent.Parent = rotated;

            if (rotationType == RotationType.Right)
            {
                parent.Left = rotated.Right;
                if (parent.Left != null)
                    parent.Left.Parent = parent;
                rotated.Right = parent;
            }
            else
            {
                parent.Right = rotated.Left;
                if (parent.Right != null)
                    parent.Right.Parent = parent;
                rotated.Left = parent;
            }
        }
    }
}
