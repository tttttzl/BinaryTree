using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace offer
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeNode node = null; 
            int[] data = new int[10]{50,30,66,100,20,40,25,28,10,4};

            BTree btree = new BTree();
            for(int i = 0;i<data.Count();i++)
            btree.AddNode(ref node,data[i]);

            //Console.WriteLine("递归中序遍历");
            //btree.MidOut(node);
            //Console.WriteLine("非递归中序遍历");
            //btree.MidOut2(node);

            //Console.WriteLine("递归中序遍历");
            //btree.PreOut(node);
            //Console.WriteLine("非递归中序遍历");
            //btree.PreOut2(node);

            Console.WriteLine("递归中序遍历");
            btree.LastOut(node);
            Console.WriteLine("非递归中序遍历");
            btree.LastOut2(node);

            Console.WriteLine("完成遍历");
                 
            Console.ReadKey();
        }
    }

    public class BTree
    {
        //public TreeNode root = null;

        //添加节点
        public void AddNode(ref TreeNode node,int data)
        {
            //先判断是不是空，是空就直接加进去
            if (node == null)
            {
                node = new TreeNode(data);
                Console.WriteLine(data);
            }
            //若不是空
            else
            {
                //比当前节点小就去左
                if (data < node.Data)
                {
                    AddNode(ref node.left,data);
                }
                //比当前节点大就去右边
                else
                {
                    AddNode(ref node.right, data);
                }
            }     
        }

        //遍历查找某数
        public TreeNode Search(TreeNode node,int number)
        {
            //对比当前节点数据
            //大了就去左边，小了就去右边
            if (node != null)
            {              
                if (node.Data > number)
                {
                    Console.WriteLine("大了");
                    return node = Search(node.left, number);
                }
                if (node.Data < number)
                {
                    Console.WriteLine("小了");
                    return node = Search(node.right, number);
                }
                else
                {
                    Console.WriteLine("正好");
                    return node;
                }
            }
            Console.WriteLine("没找到");
            return node;
        }

        //这里没有指针不好操作
        public void DeleNode(ref TreeNode root, int number)
        {
            if (root != null)
            {
                if (root.Data == number)
                {
                    //只有根节点
                    if (root.left == null && root.right == null)
                        root = null;
                    //有左子
                    else if (root.left != null && root.right == null)
                        root = root.left;
                    //有右子
                    else if (root.left == null && root.right != null)
                        root = root.right;
                    else
                    {
                        unsafe
                        {
                            //保存当前节点
                            TreeNode node = root;
                            while (true)
                            {
                                //找一个叶子节点
                                if (node.right == null && node.left == null)
                                {
                                    root.Data = node.Data;
                                    DeleNode(ref root.right,node.Data);
                                    return;
                                }
                                else if (node.right == null && node.left != null)
                                {
                                    root.Data = node.Data;
                                    DeleNode(ref root.right, node.Data);
                                    return;
                                }
                                else
                                    node = node.right;
                            }
                        }

                    }
                    return;
                }
                else
                {
                    if (root.Data > number)
                    {
                        DeleNode(ref root.left, number);
                    }
                    else
                    {
                        DeleNode(ref root.right, number);
                    }
                }
            }
            else
                return;           
        }
        //前序非递归遍历
        public void PreOut2(TreeNode node)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();

            while (stack.Count() != 0 || node != null)
            {
                while (node != null)
                {
                    Console.WriteLine(node.Data);
                    stack.Push(node);
                    node = node.left;
                }
                if (stack.Count() != 0)
                {
                    node = stack.Pop();
                    node = node.right;
                }
            }
        }
        //前序遍历
        public void PreOut(TreeNode node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Data);
                PreOut(node.left);
                PreOut(node.right);
            }
            else
                return;
        }
        //非递归中序遍历
        public void MidOut2(TreeNode node)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();

            while (stack.Count() != 0 || node != null)
            {
                while (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }
                if (stack.Count() != 0)
                {
                    node = stack.Pop();
                    Console.WriteLine(node.Data);
                    node = node.right;
                }
            }
        }
        //中序遍历
        public void MidOut(TreeNode node)
        {
            if (node.left != null)
                MidOut(node.left);
            Console.WriteLine(node.Data);
            if (node.right != null)
                MidOut(node.right);
        }

        //后序非递归遍历(这里会涉及到一个重复遍历右节点的操作所以需要一个pass来记录遍历过的节点)
        public void LastOut2(TreeNode node)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode pass = new TreeNode(0);

            while (stack.Count() != 0 || node != null)
            {
                while (node != null)
                { 
                    stack.Push(node);
                    node = node.left;
                }
                if (stack.Count() != 0)
                {
                    node = stack.Peek();

                    if (node.right != null && node.right != pass)
                    {
                        node = node.right;
                    }
                    else
                    {   
                        Console.WriteLine(stack.Pop().Data);
                        pass = node;
                        node = null;
                    }
                }
            }
        }
        //后续遍历
        public void LastOut(TreeNode node)
        {
            if (node.left != null)
                LastOut(node.left);
            if (node.right != null)
                LastOut(node.right);
            Console.WriteLine(node.Data);
        }
    }

    /// <summary>
    /// 节点类--
    /// </summary>

    public class TreeNode
    {
        public int Data = 0;
        public TreeNode left = null;
        public TreeNode right = null;

        public TreeNode(int number)
        {
            this.Data = number;
        }
    }
}
