namespace task_4
{
    public class SplayTreeNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public SplayTreeNode<T>? Right { get; set; }
        public SplayTreeNode<T>? Left { get; set; }
        public SplayTreeNode<T>? Parent { get; set; }


        public SplayTreeNode(T data)
        {
            Parent = null;
            Data = data;
        }
    }
}