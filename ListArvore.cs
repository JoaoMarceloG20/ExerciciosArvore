using System;

public class BinaryTree<T> where T : IComparable<T>
{
    public class Node
    {
        public T Value;
        public Node Left, Right;

        public Node(T value)
        {
            Value = value;
            Left = Right = null;
        }
    }

    public Node Root;

    public void Insert(T value)
    {
        Root = InsertRec(Root, value);
    }

    private Node InsertRec(Node root, T value)
    {
        if (root == null)
        {
            root = new Node(value);
            return root;
        }

        if (value.CompareTo(root.Value) < 0)
        {
            root.Left = InsertRec(root.Left, value);
        }
        else if (value.CompareTo(root.Value) > 0)
        {
            root.Right = InsertRec(root.Right, value);
        }

        return root;
    }

    public void Delete(T value)
    {
        Root = DeleteRec(Root, value);
    }

    private Node DeleteRec(Node root, T value)
    {
        if (root == null) return root;

        if (value.CompareTo(root.Value) < 0)
        {
            root.Left = DeleteRec(root.Left, value);
        }
        else if (value.CompareTo(root.Value) > 0)
        {
            root.Right = DeleteRec(root.Right, value);
        }
        else
        {
            if (root.Left == null)
            {
                return root.Right;
            }
            else if (root.Right == null)
            {
                return root.Left;
            }
            root.Value = MinValue(root.Right);
            root.Right = DeleteRec(root.Right, root.Value);
        }

        return root;
    }

    private T MinValue(Node root)
    {
        T minValue = root.Value;
        while (root.Left != null)
        {
            minValue = root.Left.Value;
            root = root.Left;
        }
        return minValue;
    }

    public int GetHeight(Node node)
    {
        if (node == null) return -1;
        return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    public int GetNodeCount(Node node)
    {
        if (node == null) return 0;
        return 1 + GetNodeCount(node.Left) + GetNodeCount(node.Right);
    }

    public int GetLeafCount(Node node)
    {
        if (node == null) return 0;
        if (node.Left == null && node.Right == null) return 1;
        return GetLeafCount(node.Left) + GetLeafCount(node.Right);
    }

    public int GetBalanceFactor(Node node)
    {
        if (node == null) return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    public bool AreEqual(Node root1, Node root2)
    {
        if (root1 == null && root2 == null) return true;
        if (root1 == null || root2 == null) return false;
        return root1.Value.CompareTo(root2.Value) == 0 && AreEqual(root1.Left, root2.Left) && AreEqual(root1.Right, root2.Right);
    }
}

class Program
{
    static void Main()
    {
        var tree1 = new BinaryTree<int>();
        var tree2 = new BinaryTree<int>();

        Console.Write("Quantos valores você deseja inserir na árvore 1? ");
        int numberOfValues = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < numberOfValues; i++)
        {
            Console.Write($"Insira o valor {i + 1} de {numberOfValues} na árvore 1: ");
            int value = Convert.ToInt32(Console.ReadLine());
            tree1.Insert(value);
            Console.WriteLine($"{value} inserido na árvore 1.");
        }

        bool continueRunning = true;

        while (continueRunning)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Exibir altura da árvore 1");
            Console.WriteLine("2. Exibir contagem de nós da árvore 1");
            Console.WriteLine("3. Exibir contagem de folhas da árvore 1");
            Console.WriteLine("4. Exibir fator de balanceamento da árvore 1");
            Console.WriteLine("5. Inserir outro valor na árvore 1");
            Console.WriteLine("6. Remover um valor da árvore 1");
            Console.WriteLine("7. Comparar árvore 1 com outra árvore");
            Console.WriteLine("8. Sair");
            Console.Write("Escolha uma opção: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Altura da árvore 1: " + tree1.GetHeight(tree1.Root));
                    break;
                case 2:
                    Console.WriteLine("Contagem de nós da árvore 1: " + tree1.GetNodeCount(tree1.Root));
                    break;
                case 3:
                    Console.WriteLine("Contagem de folhas da árvore 1: " + tree1.GetLeafCount(tree1.Root));
                    break;
                case 4:
                    Console.WriteLine("Fator de balanceamento da árvore 1: " + tree1.GetBalanceFactor(tree1.Root));
                    break;
                case 5:
                    Console.Write("Informe o valor a ser inserido na árvore 1: ");
                    int valueToInsert = Convert.ToInt32(Console.ReadLine());
                    tree1.Insert(valueToInsert);
                    Console.WriteLine($"{valueToInsert} inserido na árvore 1.");
                    break;
                case 6:
                    Console.Write("Informe o valor a ser removido da árvore 1: ");
                    int valueToRemove = Convert.ToInt32(Console.ReadLine());
                    tree1.Delete(valueToRemove);
                    Console.WriteLine($"{valueToRemove} removido da árvore 1.");
                    break;
                case 7:
                    Console.WriteLine("Comparando árvore 1 com outra árvore...");
                    bool areTreesEqual = tree1.AreEqual(tree1.Root, tree2.Root); 
                    Console.WriteLine("As árvores são iguais: " + areTreesEqual);
                    break;
                case 8:
                    Console.WriteLine("Saindo...");
                    continueRunning = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}