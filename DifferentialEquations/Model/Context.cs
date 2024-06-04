using Model.Solvers;
using System.Runtime.InteropServices;

namespace Model
{
    public class Context
    {
        MLApp.MLApp matlab;

        public IODESolver Solver { get; set; }

        public string FunctionsDir
        {
            get
            {
                return Environment.CurrentDirectory + "\\Functions";
            }
        }

        public void SolveODE(double y0)
        {
            if (Solver == null) return;

            matlab.Execute("X = []");
            matlab.Execute("Y = []");

            matlab.Execute("y = " + y0);

            Solver.MatLab = matlab;
            Solver.Solve();

            matlab.Execute("plot(X, Y)");
        }

        public void SetFunc(string fx)
        {
            matlab.Execute("clear all");

            fx = fx.Replace("^", ".^");

            StreamWriter stream = new StreamWriter(File.Create(FunctionsDir + "\\d.m"));

            stream.WriteLine("function f = d(x, y)");
            stream.WriteLine("f = " + fx + ";");
            stream.WriteLine("end");

            stream.Close();
        }

        public Context()
        {
            matlab = new MLApp.MLApp();
            matlab.Visible = 0;

            matlab.Execute("clear all");

            Directory.CreateDirectory(FunctionsDir);
            matlab.Execute("cd " + FunctionsDir);

            SetFunc("x^2");
        }

        ~Context()
        {
            WeakReference matlabRef = new WeakReference(matlab);

            if (matlabRef != null && 
                matlabRef.Target != null && 
                matlabRef.IsAlive)
            {
                Marshal.FinalReleaseComObject(matlabRef.Target);
            }
        }
    }
}
