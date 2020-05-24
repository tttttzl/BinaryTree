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
            Console.WriteLine("前序遍历");
            btree.PreOut(node);
            Console.WriteLine("删除节点");
            btree.DeleNode(ref node, 20);
            Console.WriteLine("前序遍历");
            btree.PreOut(node);
            Console.WriteLine("查找某数");
            btree.Search(node,20);           
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
        //中序遍历
        public void MidOut(TreeNode node)
        {
            if (node.left != null)
                MidOut(node.left);
            Console.WriteLine(node.Data);
            if (node.right != null)
                MidOut(node.right);
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
