namespace CourseProject.Codebase.Disposable
{
    public class Disposer
    {
        private const string _assemblyName = "RudolfKursovaya";
        private System.Reflection.Assembly _sharpAssembly;

        private IDispose[] _disposeClasses = new IDispose[0];
        
        public Disposer()
        {
            //_sharpAssembly = AppDomain.CurrentDomain.GetAssemblies().First(atr => atr.GetName().Name.Equals(_assemblyName));
            
            //GetDisposeClasses();
        }

        public void Dispose()
        {
            for (int i = 0; i < _disposeClasses.Length; i++) 
                _disposeClasses[i].Dispose();
            
            Console.WriteLine("All disposed");
        }
        
        private void GetDisposeClasses()
        {
            List<Type> signalTypes = GetTypesWithAttribute<DisposeAttribute, IDispose>();

            if (signalTypes.Count > 0)
            {
                _disposeClasses = new IDispose[signalTypes.Count];
             
                for (int i = 0; i < signalTypes.Count; i++) 
                    _disposeClasses[i] = signalTypes[i] as IDispose;
            }
        }
        
        private List<Type> GetTypesWithAttribute<TAttribute, TType>()
        {
            List<Type> types = new List<Type>();
 
            foreach(Type type in _sharpAssembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(TAttribute), true).Length > 0 &&
                    !type.Name.Equals(typeof(TType).Name))
                    types.Add(type);
            }
 
            return types;
        }
    }
}