using System;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            string tree = "A(B(C(E(H,),F),D(,I)),J(K,L(M(,N),)))";
            TwoXTree twoXTree = TwoXTree.CreateTwoXTree(tree);

            int totalCount = twoXTree.GetTotalCount();
            int totalCountRecursion = twoXTree.GetTotalCount_Recursion();

            int maxLength = twoXTree.GetMaxLength();
            int maxLengthRecursion = twoXTree.GetMaxLength_Recursion();

            Console.WriteLine("Preorder: ");
            twoXTree.Preorder_Recursion();
            Console.WriteLine();
            twoXTree.Preorder();
            Console.WriteLine();

            Console.WriteLine("Inorder: ");
            twoXTree.Inorder_Recursion();
            Console.WriteLine();
            twoXTree.Inorder();
            Console.WriteLine();

            Console.WriteLine("Posorder: ");
            twoXTree.Posorder_Recursion();
            Console.WriteLine();
            twoXTree.Posorder();
            Console.WriteLine();

            Console.WriteLine("Layer Order: ");
            twoXTree.LayerOrder();
            Console.WriteLine();

            Console.ReadKey();
            
        }
    }
}
