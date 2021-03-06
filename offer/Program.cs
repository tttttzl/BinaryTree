﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace offer
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();

            BTree btree = new BTree();
            //TreeNode node = btree.ReCreatBTree(new int[] { 1,2,3,4,5,6},new int[] {6,5,4,3,2,1},0,0,6); 
            int[] data = new int[10]{50,30,66,100,20,40,25,28,10,4};
            TreeNode node = null;

            for(int i = 0;i<data.Count();i++)
            btree.AddNode(ref node,data[i]);

            //Console.WriteLine("叶子节点个数");
            //Console.WriteLine(btree.GetYeziNode(node));

            //Console.WriteLine("最大深度");
            //Console.WriteLine(btree.GetMaxHight(node));

            //Console.WriteLine("最小深度");
            //Console.WriteLine(btree.GetMinHight(node));

            //Console.WriteLine("递归中序遍历");
            //btree.MidOut(node);
            //Console.WriteLine("非递归中序遍历");
            //btree.MidOut2(node);

            Console.WriteLine("递归中序遍历");
            btree.MidOut(node);

            Console.WriteLine("删除");
            btree.DeleNode(ref node, 20);

            Console.WriteLine("递归中序遍历");
            btree.MidOut(node);
            //Console.WriteLine("非递归中序遍历");
            //btree.PreOut2(node);

            //Console.WriteLine("递归中序遍历");
            //btree.LastOut(node);
            //Console.WriteLine("非递归中序遍历");
            //btree.LastOut2(node);

            Console.WriteLine("完成遍历");
                 
            Console.ReadKey();
        }
    }
    public class BTree
    {
        //public TreeNode root = null;

        //添加节点
        public void AddNode(ref TreeNode node, int data)
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
                    AddNode(ref node.left, data);
                }
                //比当前节点大就去右边
                else
                {
                    AddNode(ref node.right, data);
                }
            }
        }

        /// <summary>
        /// 前序/中序  重构二叉树
        /// </summary>
        /// <param name="pre"></前序遍历>
        /// <param name="mid"></中序遍历>
        /// <param name="P_Start"></前序当前树开始下标>
        /// <param name="M_Start"></中序当前树开始下标>
        /// <param name="Length"></树节点数，用于循环>
        /// <returns></returns>
        public TreeNode ReCreatBTree(int[] pre,int[] mid,int P_Start,int M_Start , int Length)
        {
            if (P_Start<0 || M_Start<0 || Length <= 0 )
                return null;

            TreeNode node = new TreeNode(pre[P_Start]);
            for (int i = M_Start; i < M_Start+Length; i++)
            {
                if (mid[i] == node.Data)
                {
                    node.left = ReCreatBTree(pre, mid, P_Start + 1, M_Start,i-M_Start);
                    node.right = ReCreatBTree(pre,mid, P_Start + 1+(i- M_Start),i+1,Length - 1 -(i - M_Start));
                    break;
                }
            }
            return node;
        }

        //遍历查找某数
        public TreeNode Search(TreeNode node, int number)
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
                            TreeNode node = root.left;
                            
                            while (true)
                            {
                                //找一个叶子节点
                                if (node.right == null)
                                {
                                    root.Data = node.Data;
                                    DeleNode(ref root.left, node.Data);
                                    break;
                                }
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

        //寻找最小深度
        public int GetMinHight(TreeNode node)
        {
            if (node == null)
                return 0;
            if (node.left == null && node.right == null)
                return 1;
            if (node.left == null)
                return GetMinHight(node.right) + 1;
            if (node.right == null)
                return GetMinHight(node.left) + 1;

            return Math.Min(GetMinHight(node.left), GetMinHight(node.right)) + 1;
        }

        //寻找最大深度
        public int GetMaxHight(TreeNode node)
        {
            if (node == null)
                return 0;
            return Math.Max(GetMaxHight(node.left), GetMaxHight(node.right)) + 1;
        }

        //寻找叶子节点个数
        public int GetYeziNode(TreeNode node)
        {
            if (node == null)
                return 0;
            if (node.left == null && node.right == null)
                return 1;
            return GetYeziNode(node.left) + GetYeziNode(node.right);
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
