namespace GA4lib.GP
{
    public enum ExpressionType
    {
        //Constant = 0,
        Variable,

        Sum,
        Sub,
        Mul,
        Div,
        Pow,

        Ln,
        Cos,
        Sin,
        Sqrt,
        Exp
    }
    public class Expression
    {
        public ExpressionType Operation { get; set; }
        public string? Variable { get; set; }
        public double? Value { get; set; }
        public List<Expression> Childs { get; set; } = new();
        public Expression(ExpressionType operation) => this.Operation = operation;

        public string ToString(GpContext context)
        {
            return Operation switch
            {
                //ExpressionType.Constant => $"({(double)Value!})",
                ExpressionType.Variable => $"{Variable!}",
                ExpressionType.Sum => $"({Childs[0].ToString(context)} + {Childs[1].ToString(context)})",
                ExpressionType.Sub => $"({Childs[0].ToString(context)} - {Childs[1].ToString(context)})",
                ExpressionType.Mul => $"({Childs[0].ToString(context)} * {Childs[1].ToString(context)})",
                ExpressionType.Div => $"({Childs[0].ToString(context)} / {Childs[1].ToString(context)})",
                ExpressionType.Pow => $"({Childs[0].ToString(context)} ^ {Childs[1].ToString(context)})",
                ExpressionType.Ln => $"ln({Childs[0].ToString(context)})",
                ExpressionType.Cos => $"cos({Childs[0].ToString(context)})",
                ExpressionType.Sin => $"sin({Childs[0].ToString(context)})",
                ExpressionType.Sqrt => $"sqrt({Childs[0].ToString(context)})",
                ExpressionType.Exp => $"exp({Childs[0].ToString(context)})"
            };
        }

        public string ToStringOnlySelf()
        {
            return Operation switch
            {
                //ExpressionType.Constant => $"({(double)Value!})",
                ExpressionType.Variable => $"{Variable!}",
                ExpressionType.Sum => $"sum",
                ExpressionType.Sub => $"sub",
                ExpressionType.Mul => $"mul",
                ExpressionType.Div => $"div",
                ExpressionType.Pow => $"pow",
                ExpressionType.Ln => $"ln",
                ExpressionType.Cos => $"cos",
                ExpressionType.Sin => $"sin",
                ExpressionType.Sqrt => $"sqrt",
                ExpressionType.Exp => $"exp"
            };
        }
        public double Eval(GpContext context)
        {
            double res = Operation switch
            {
                //ExpressionType.Constant => (double)Value!,
                ExpressionType.Variable => context.Variables[Variable!],
                ExpressionType.Sum => Childs[0].Eval(context) + Childs[1].Eval(context),
                ExpressionType.Sub => Childs[0].Eval(context) - Childs[1].Eval(context),
                ExpressionType.Mul => Childs[0].Eval(context) * Childs[1].Eval(context),
                ExpressionType.Div => Childs[0].Eval(context) / Childs[1].Eval(context),
                ExpressionType.Pow => Math.Pow(Math.Abs(Childs[0].Eval(context)), Childs[1].Eval(context)),
                ExpressionType.Ln => Math.Log(Math.Abs(Childs[0].Eval(context))),
                ExpressionType.Cos => Math.Cos(Childs[0].Eval(context)),
                ExpressionType.Sin => Math.Sin(Childs[0].Eval(context)),
                ExpressionType.Sqrt => Math.Sqrt(Math.Abs(Childs[0].Eval(context))),
                ExpressionType.Exp => Math.Exp(Childs[0].Eval(context))
            };
            return res;
        }
        public Expression Clone()
        {
            Expression clonedExpression = new Expression(Operation);
            clonedExpression.Variable = Variable;
            clonedExpression.Value = Value;
            clonedExpression.Childs = Childs.Select(it => it.Clone()).ToList();
            return clonedExpression;
        }
    }
}
