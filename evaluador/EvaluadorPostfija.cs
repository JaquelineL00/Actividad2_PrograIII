using System;
using System.Collections.Generic;

namespace Actividad2.evaluador
{
    public class EvaluadorPostfija
    {
        public event Action OnOutputChanged;
        private double _resultado;

        public double Resultado
        {
            get => _resultado;
            private set
            {
                _resultado = value;
                OnOutputChanged?.Invoke();
            }
        }

        public void EvaluarExpresionPostfija(string expresionPostfija)
        {
            Stack<double> pila = new Stack<double>();
            string[] tokens = expresionPostfija.Split(' ');

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double numero))
                {
                    pila.Push(numero);
                }
                else if ("+-*/".Contains(token) && pila.Count >= 2)
                {
                    double operando2 = pila.Pop();
                    double operando1 = pila.Pop();
                    double resultado = token switch
                    {
                        "+" => operando1 + operando2,
                        "-" => operando1 - operando2,
                        "*" => operando1 * operando2,
                        "/" => operando2 != 0 ? operando1 / operando2 : throw new DivideByZeroException("División por cero."),
                        _ => throw new InvalidOperationException("Operador desconocido")
                    };
                    pila.Push(resultado);
                }
                else
                {
                    throw new InvalidOperationException("Expresión postfija inválida");
                }
            }

            if (pila.Count == 1)
            {
                Resultado = pila.Pop();
            }
            else
            {
                throw new InvalidOperationException("Expresión postfija mal formada");
            }
        }
    }
}