namespace CourseProject.Codebase.Disposable
{
    public class Disposer // класс "очиститель"
    {
        private const string _assemblyName = "RudolfKursovaya"; // имя сборки курсовой
        private System.Reflection.Assembly _sharpAssembly; // имя сборки C#

        private IDispose[] _disposeClasses = new IDispose[0]; // кэшированные классы требующие очистку
        
        public Disposer() // конструктор класса
        {
            //_sharpAssembly = AppDomain.CurrentDomain.GetAssemblies().First(atr => atr.GetName().Name.Equals(_assemblyName));
            //GetDisposeClasses();
        }

        public void Dispose() // метод очистки
        {
            for (int i = 0; i < _disposeClasses.Length; i++) // проходим по кешированным классам
                _disposeClasses[i].Dispose(); // вызываем у них метод очистки
        }
        
        private void GetDisposeClasses() // получаем классы с атрибутом [DisposeAttribute]
        {
            List<Type> signalTypes = GetTypesWithAttribute<DisposeAttribute, IDispose>();

            if (signalTypes.Count > 0)
            {
                _disposeClasses = new IDispose[signalTypes.Count];
             
                for (int i = 0; i < signalTypes.Count; i++) 
                    _disposeClasses[i] = signalTypes[i] as IDispose;
            }
        }
        
        private List<Type> GetTypesWithAttribute<TAttribute, TType>() // метод получения классов через рефлексию
        {
            List<Type> types = new List<Type>(); // список типов
 
            foreach(Type type in _sharpAssembly.GetTypes()) // получаем у сборки классы и проходим по ним
            {
                if (type.GetCustomAttributes(typeof(TAttribute), true).Length > 0 && // проверяем кол-во пользовательских атрибутов
                    !type.Name.Equals(typeof(TType).Name)) // избегаем тип самого атрибута
                    types.Add(type); // добавляем тип в список
            }
 
            return types; // возвращаем список с типами
        }
    }
}