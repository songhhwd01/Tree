using System;
using System.Collections.Generic;
using System.Linq;

namespace Tree
{
    public class TwoXTree
    {
        public string Data { set; get; }
        public TwoXTree LeftChild { set; get; }
        public TwoXTree RightChild { set; get; }

        public TwoXTree()
        {
        }

        /// <summary>
        /// Create a 2XTree
        /// </summary>
        /// <param name="treeString">eg: A(B(C,D),E(,F(G,)))$</param>
        /// <returns></returns>
        public static TwoXTree CreateTwoXTree(string treeString)
        {
            if (String.IsNullOrEmpty(treeString) || treeString.Equals("$"))
            {
                return null;
            }

            var charArray = treeString.ToCharArray();
            if (charArray.Count(item => item == '(') != charArray.Count(item => item == ')'))
            {
                throw new ArgumentException("treeString");
            }

            Stack<TwoXTree> stack = new Stack<TwoXTree>();
            TwoXTree root = new TwoXTree
            {
                Data = treeString[0].ToString()
            };
            stack.Push(root);

            TwoXTree parent = root, node = root;            
            bool isLeftNode = true;
            for (int i = 1; i < treeString.Length; i++)
            {
                switch (treeString[i])
                {
                    case '$':
                        break;
                    case '(':
                        stack.Push(node);
                        parent = node;
                        isLeftNode = true;
                        break;
                    case ')':
                        stack.Pop();
                        parent = stack.Peek();
                        break;
                    case ',':
                        isLeftNode = false;
                        break;
                    default:                        
                        node = new TwoXTree
                        {
                            Data = treeString[i].ToString()
                        };
                        
                        if (parent != null)
                        {
                            if (isLeftNode)
                            {
                                parent.LeftChild = node;
                            }
                            else
                            {
                                parent.RightChild = node;
                            }
                        }
                        break;
                }
            }

            return root;
        }

        #region Get total count
        /// <summary>
        /// Get total count by recursion
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount_Recursion()
        {
            int totalCount = 1;

            if (this.LeftChild != null)
                totalCount += this.LeftChild.GetTotalCount_Recursion();
            if (this.RightChild != null)
                totalCount += this.RightChild.GetTotalCount_Recursion();

            return totalCount;
        }

        /// <summary>
        /// Get total count
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            Queue<TwoXTree> queue = new Queue<TwoXTree>();
            TwoXTree node = this;
            int totalCount = 0;
            while (node != null)
            {
                ++totalCount;

                if (node.LeftChild != null)
                    queue.Enqueue(node.LeftChild);
                if (node.RightChild != null)
                    queue.Enqueue(node.RightChild);

                if (queue.Count > 0)
                {
                    node = queue.Dequeue();
                }
                else
                {
                    node = null;
                }
            }

            return totalCount;
        }
        #endregion

        #region Get max length
        public int GetMaxLength_Recursion()
        {
            int left = 0, right = 0;
            if (this.LeftChild != null)
                left = this.LeftChild.GetMaxLength_Recursion();
            if (this.RightChild != null)
                right = this.RightChild.GetMaxLength_Recursion();

            return left > right ? left + 1 : right + 1;
        }

        public int GetMaxLength()
        {
            Queue<TwoXTree> treeQueue = new Queue<TwoXTree>();
            Queue<int> lengthQueue = new Queue<int>();

            TwoXTree node = this;
            int length = 0;
            treeQueue.Enqueue(node);
            lengthQueue.Enqueue(1);
            while (treeQueue.Count > 0)
            {
                node = treeQueue.Dequeue();
                length = lengthQueue.Dequeue();

                if (node.LeftChild != null)
                {
                    treeQueue.Enqueue(node.LeftChild);
                    lengthQueue.Enqueue(length + 1);
                }
                if (node.RightChild != null)
                {
                    treeQueue.Enqueue(node.RightChild);
                    lengthQueue.Enqueue(length + 1);
                }
            }

            return length;
        }
        #endregion

        private void Visit(TwoXTree treeNode)
        {
            if (treeNode == null)
                return;

            Console.Write(" ---> {0} ", treeNode.Data);
        }

        #region Preorder traversal
        public void Preorder_Recursion()
        {
            Visit(this);
            if (this.LeftChild != null)
            {
                this.LeftChild.Preorder_Recursion();
            }
            if (this.RightChild != null)
            {
                this.RightChild.Preorder_Recursion();
            }
        }

        public void Preorder()
        {
            Stack<TwoXTree> stack = new Stack<TwoXTree>();
            Stack<bool> stack2 = new Stack<bool>();
            TwoXTree node = this;
            bool flag;  // used to check if already visit both the left child and the right child of current node

            do
            {
                while (node != null)
                {
                    Visit(node);
                    stack.Push(node);
                    stack2.Push(false);

                    node = node.LeftChild;
                }

                node = stack.Pop();
                flag = stack2.Pop();
                if (flag)
                {
                    node = null;
                }
                else
                {
                    stack.Push(node);
                    stack2.Push(true);

                    node = node.RightChild;
                }
            } while (node != null || stack.Count > 0);
        }
        #endregion

        #region Inorder
        public void Inorder_Recursion()
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.Inorder_Recursion();
            }
            Visit(this);
            if (this.RightChild != null)
            {
                this.RightChild.Inorder_Recursion();
            }
        }

        public void Inorder()
        {
            Stack<TwoXTree> stack = new Stack<TwoXTree>();
            Stack<bool> stack2 = new Stack<bool>();
            TwoXTree node = this;
            bool flag;

            do
            {
                while (node != null)
                {
                    stack.Push(node);
                    stack2.Push(false);

                    node = node.LeftChild;
                }

                node = stack.Pop();
                flag = stack2.Pop();
                if (flag)
                {
                    node = null;
                }
                else
                {
                    Visit(node);
                    stack.Push(node);
                    stack2.Push(true);

                    node = node.RightChild;
                }
            } while (node != null || stack.Count > 0);
        }
        #endregion

        #region Posorder
        public void Posorder_Recursion()
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.Posorder_Recursion();
            }
            if (this.RightChild != null)
            {
                this.RightChild.Posorder_Recursion();
            }
            Visit(this);
        }

        public void Posorder()
        {
            Stack<TwoXTree> stack = new Stack<TwoXTree>();
            Stack<bool> stack2 = new Stack<bool>();
            TwoXTree node = this;
            bool flag;

            do
            {
                while (node != null)
                {
                    stack.Push(node);
                    stack2.Push(false);

                    node = node.LeftChild;
                }

                node = stack.Pop();
                flag = stack2.Pop();
                if (flag)
                {
                    Visit(node);
                    node = null;
                }
                else
                {
                    stack.Push(node);
                    stack2.Push(true);

                    node = node.RightChild;
                }

            } while (node != null || stack.Count > 0);
        }
        #endregion

        #region Layer Order
        public void LayerOrder()
        {
            Queue<TwoXTree> queue = new Queue<TwoXTree>();            
            queue.Enqueue(this);

            TwoXTree node;
            while (queue.Count > 0)
            {
                node = queue.Dequeue();

                Visit(node);
                if (node.LeftChild != null)
                    queue.Enqueue(node.LeftChild);
                if (node.RightChild != null)
                    queue.Enqueue(node.RightChild);
            }
        }
        #endregion
    }
}
