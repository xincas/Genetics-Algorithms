using System.Text;

namespace GA4lib.GP
{
    public static class SyntaxTreeUtils
    {
        public static Expression CreateTree(int depth, GpContext context)
        {
            if (depth > 1)
            {
                ExpressionType operation =
                    (ExpressionType)Random.Shared.Next(Enum.GetNames(typeof(ExpressionType)).Length);
                Expression expression = new Expression(operation);

                switch (operation)
                {
                    //case ExpressionType.Constant:
                    //    expression.Value = context.GetRandomValue();
                    //    break;

                    case ExpressionType.Variable:
                        expression.Variable =
                            context.Variables.Keys.ElementAt(Random.Shared.Next(context.Variables.Keys.Count));
                        break;

                    case ExpressionType.Div:
                    case ExpressionType.Mul:
                    case ExpressionType.Sub:
                    case ExpressionType.Sum:
                    case ExpressionType.Pow:
                        expression.Childs.Add(CreateTree(depth - 1, context));
                        expression.Childs.Add(CreateTree(depth - 1, context));
                        break;

                    case ExpressionType.Cos:
                    case ExpressionType.Sin:
                    case ExpressionType.Ln:
                    case ExpressionType.Sqrt:
                    case ExpressionType.Exp:
                        expression.Childs.Add(CreateTree(depth - 1, context));
                        break;
                }

                return expression;
            }
            else
            {
                //ExpressionType operation = (ExpressionType)Random.Shared.Next(2);
                ExpressionType operation = ExpressionType.Variable;
                Expression expression = new Expression(operation);

                //if (operation == ExpressionType.Constant)
                //    expression.Value = context.GetRandomValue();
                //else
                    expression.Variable =
                        context.Variables.Keys.ElementAt(Random.Shared.Next(context.Variables.Keys.Count));

                return expression;
            }
        }

        public static void SimplifyTree(Expression tree, GpContext context)
        {
            if(HasVariableNode(tree))
                foreach (Expression child in tree.Childs)
                    SimplifyTree(child, context);
            else
            {
                double value = tree.Eval(context);
                tree.Value = value;
                tree.Childs.Clear();
                //tree.Operation = ExpressionType.Constant;
            }
        }

        public static void CutTree(Expression tree, GpContext context, int depth)
        {
            if (depth > 1)
                foreach (Expression child in tree.Childs)
                    CutTree(child, context, depth - 1);
            else
            {
                tree.Childs.Clear();
                tree.Value = null;
                tree.Variable = null;

                //ExpressionType operation = (ExpressionType)Random.Shared.Next(2);
                ExpressionType operation = ExpressionType.Variable;
                tree.Operation = operation;

                //if (operation == ExpressionType.Constant)
                //    tree.Value = context.GetRandomValue();
                //else
                    tree.Variable =
                        context.Variables.Keys.ElementAt(Random.Shared.Next(context.Variables.Keys.Count));
            }
        }

        public static List<Expression> GetAllNodeAsList(Expression tree)
        {
            List<Expression> nodes = new();
            GetAllNodesBreadthFirstSearch(tree, nodes);
            return nodes;
        }

        public static void GetAllNodesBreadthFirstSearch(Expression currentExpression, List<Expression> nodesList)
        {
            int indx = 0;
            nodesList.Add(currentExpression);
            while (true)
            {
                if (indx < nodesList.Count)
                {
                    Expression node = nodesList[indx++];
                    foreach (Expression child in node.Childs) 
                        nodesList.Add(child);
                }
                else
                    break;
            }
        }

        public static void SwapNode(Expression oldNode, Expression newNode)
        {
            oldNode.Childs = newNode.Childs;
            oldNode.Operation = newNode.Operation;
            oldNode.Value = newNode.Value;
            oldNode.Variable = newNode.Variable;
        }

        public static Expression GetRandomNode(Expression tree)
        {
            List<Expression> allNodes = GetAllNodeAsList(tree);
            return allNodes[Random.Shared.Next(0, allNodes.Count)];
        }

        public static void SetCoefficients(Expression tree, List<double> coefficients)
        {
            int index = 0;
            SetCoefficients(tree, coefficients, ref index);
        }

        private static void SetCoefficients(Expression current, List<double> coefficients, ref int index)
        {
            if (current.Value is not null) current.Value = coefficients[index++];
            foreach (Expression child in current.Childs) SetCoefficients(child, coefficients, ref index);
        }

        public static List<double> GetCoefficients(Expression tree)
        {
            List<double> coefficients = new();
            GetCoefficients(tree, coefficients);
            return coefficients;
        }

        public static int GetHeight(Expression tree)
        {
            if (tree.Childs.Count == 0)
                return 1;
            else if (tree.Childs.Count == 1)
                return 1 + GetHeight(tree.Childs[0]);
            else
                return 1 + Math.Max(GetHeight(tree.Childs[0]), GetHeight(tree.Childs[1]));
        }

        private static void GetCoefficients(Expression current, List<double> coefficients)
        {
            if (current.Value is not null) coefficients.Add((double)current.Value);
            foreach (Expression child in current.Childs) GetCoefficients(child, coefficients);
        }

        private static bool HasVariableNode(Expression tree)
        {
            bool ret = false;
            if (tree.Variable is not null) 
                ret = true;
            else
                foreach (Expression child in tree.Childs)
                {
                    ret = HasVariableNode(child);
                    if (ret)
                        break;
                }

            return ret;
        }
    }
}
